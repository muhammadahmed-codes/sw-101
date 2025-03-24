using System.IO.Pipelines;
using System.Runtime.CompilerServices;

enum State {
    CLOSED,
    OPEN,
    HALF_OPEN
}

class CircuitBreaker {
    int FailureThreshold;
    int Failures;
    long LastFailureTime;
    int RecoveryTimeout;
    State State;

    CircuitBreaker(int failureThreshold, int recoveryTimeout) {
        FailureThreshold = failureThreshold;
        Failures = 0;
        LastFailureTime = 0;
        RecoveryTimeout = recoveryTimeout;
        State = State.CLOSED;
    }

    public async Task<string> Call(Func<Task<string>> action) {
        if (State == State.OPEN) {
            if (LastFailureTime > 0 && (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - LastFailureTime > RecoveryTimeout)) {
                State = State.HALF_OPEN;
                Failures = 0;
                LastFailureTime = 0;
                Console.WriteLine("Circuit is CLOSED");
            } else {
                throw new Exception("Circuit is OPEN");
            }
        }

        try {
            string result = await action();
            if (State == State.HALF_OPEN) {
                State = State.CLOSED;
                Failures = 0;
                LastFailureTime = 0;
            } 
            return result;
        } catch(Exception E) {
            Console.WriteLine("Exception: " + E.Message);
            LastFailureTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Failures++;

            if (Failures >= FailureThreshold) {
                State = State.OPEN;
            }

            throw;
        }
    }

    public Task<string> ServiceA() {
        return Task.Run(() => {
            if (new Random().NextDouble() < 0.01) {
                throw new Exception("Service failed.");
            } else {
                return "Service Response";
            }
        });
    }

    public async static Task Main(string[] args) {
        CircuitBreaker Breaker = new(3, 5000);
        
        for (int i = 0; i < 20; i++) {
            try {
                string result = await Breaker.Call(Breaker.ServiceA);
                Console.WriteLine("Sucess: " + result);
            } catch (Exception E) {
                Console.WriteLine("Failure: " + E.Message);
            }
            await Task.Delay(1000);
        }
    }
}