public class NewOrderSystem {
    private final NewPaymentGateway paymentGateway;

    public NewOrderSystem(NewPaymentGateway paymentGateway) {
        this.paymentGateway = paymentGateway;
    }

    public String placeOrderAndPay(String orderId, double amount, String currency) {
        return paymentGateway.createPayment(orderId, amount, currency);
    }

    public String checkPayment(String paymentId) {
        return paymentGateway.getPaymentStatus(paymentId);
    }

    public boolean cancelPayment(String paymentId) {
        return paymentGateway.cancelPayment(paymentId);
    }
}
