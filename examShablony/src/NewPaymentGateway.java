public interface NewPaymentGateway {
    String createPayment(String orderId, double amount, String currency);
    String getPaymentStatus(String paymentId);
    boolean cancelPayment(String paymentId);
}
