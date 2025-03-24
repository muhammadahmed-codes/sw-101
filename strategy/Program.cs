public class Strategy {

    public class Bill {
        required public string BillId; 
        required public int BillAmount;
    }

    public class PayPalBill : Bill {
    }

    public class StripeBill : Bill {
        required public string Signature;
    }
    
    public abstract class DigitalBillingStrategy<T> {
        public abstract T GenerateBill(T BillObject);
        public abstract void PrintBill(T bill);
    }

    public class PayPalBilling : DigitalBillingStrategy<PayPalBill> {
        public override PayPalBill GenerateBill(PayPalBill PBill) {
            return new() {
                BillId = PBill.BillId,
                BillAmount = PBill.BillAmount
            };
        }
        public override void PrintBill(PayPalBill bill) {
            Console.WriteLine("Bill Id: " + bill.BillId);
            Console.WriteLine("Amount : " + bill.BillAmount);
        }
    }

    public class StripeBilling : DigitalBillingStrategy<StripeBill> {
        public override StripeBill GenerateBill(StripeBill SBill) {
            return new() {
                BillId = SBill.BillId,
                BillAmount = SBill.BillAmount,
                Signature = SBill.Signature
            };
        }

        public override void PrintBill(StripeBill bill) {
            Console.WriteLine("Bill Id  : " + bill.BillId);
            Console.WriteLine("Amount   : " + bill.BillAmount);
            Console.WriteLine("Signature: " + bill.Signature);
        }
    }

    static void Main(string[] args) {
        PayPalBilling PayPalBilling = new();
        PayPalBill PayPalBill1 = new() {
            BillId = "ID0",
            BillAmount = 2000
        };
        PayPalBilling.GenerateBill(PayPalBill1);
        PayPalBilling.PrintBill(PayPalBill1);

        Console.WriteLine();
        
        StripeBilling StripeBilling = new();
        StripeBill StripeBill1 = new() {
            BillId = "ID1",
            BillAmount = 3000,
            Signature = "wxcw324351cffr"
        };
        StripeBilling.GenerateBill(StripeBill1);
        StripeBilling.PrintBill(StripeBill1);
    }
}