public interface Logger {
    public void Log(string LogMessage);
}

public class ConsoleLogger : Logger {
    
    public void Log(string LogMessage) {
        Console.WriteLine("[ConsoleLogger] -> " + LogMessage);
    }
}

public class UserService {
    private Logger LoggerObj;
    
    public UserService(Logger LoggerObj) {
        this.LoggerObj = LoggerObj;
    }
    public void SetUsername(string Username) {
        this.LoggerObj.Log("Username set to " + Username);
    }
}

public class DependencyInjection {
    public static void Main(string[] args) {
        ConsoleLogger ConsoleLogger = new();
        UserService UserService = new(ConsoleLogger);
        UserService.SetUsername("Muhammad Ahmed");
    }
}