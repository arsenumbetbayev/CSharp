public class Main {
    public static void main(String[] args) {

        OldPaymentModule oldModule = new OldPaymentModule();
        NewPaymentGateway adapter = new Adapter(oldModule);

        NewOrderSystem oms = new NewOrderSystem(adapter);

        String paymentId = oms.placeOrderAndPay("A-1001", 12500.50, "KZT");
        System.out.println("Payment created: " + paymentId);

        String status = oms.checkPayment(paymentId);
        System.out.println("Payment status: " + status);

        boolean cancelled = oms.cancelPayment(paymentId);
        System.out.println("Cancelled: " + cancelled);

        System.out.println("Payment status after cancel: " + oms.checkPayment(paymentId));
    }
}