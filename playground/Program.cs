public class Strategy {

    public class Bill {
        required public string BillId; 
        required public int BillAmount;
    }

    public class PayPalBill : Bill {

    }

    public class StripeBill : Bill{
        required public string Signature;
    }
    
    public abstract class DigitalBillingStrategy<T> {
        public abstract T GenerateBill(string Id, int Amount);
        public abstract void PrintBill(T bill);
    }

    public class PayPalBilling : DigitalBillingStrategy<PayPalBill> {
        public override PayPalBill GenerateBill(string Id, int Amount) {
            return new() {
                BillId = Id,
                BillAmount = Amount
            };
        }
        public override void PrintBill(PayPalBill bill) {
            Console.WriteLine("Bill Id: " + bill.BillId);
            Console.WriteLine("Amount : " + bill.BillAmount);
        }
    }

    // public class StripeBilling : DigitalBillingStrategy<StripeBill> {
    //     public override StripeBill GenerateBill(string Id, int Amount) {
            
    //     }

    //     public override void PrintBill(Bill bill) {
    //     }
    // }

    static void Main(string[] args) {
        PayPalBilling PayPalBilling1 = new();
        PayPalBill PayPalBill1 = PayPalBilling1.GenerateBill("BillId1", 3000);
        PayPalBilling1.PrintBill(PayPalBill1);
    }
}