public class Adapter implements NewPaymentGateway{
    private final OldPaymentModule old;

    public Adapter(OldPaymentModule legacy) {
        this.old = legacy;
    }

    @Override
    public String createPayment(String orderId, double amount, String currency) {
        String oldOrderRef = "ORD-" + orderId;
        int amountInCents = (int) Math.round(amount * 100.0);
        int currencyCode = mapCurrencyToLegacyCode(currency);

        String oldPaymentRef = old.makePay(oldOrderRef, amountInCents, currencyCode);
        return oldPaymentRef;
    }

    @Override
    public String getPaymentStatus(String paymentId) {
        String legacyStatus = old.checkPay(paymentId);
        return mapLegacyStatusToNewStatus(legacyStatus);
    }

    @Override
    public boolean cancelPayment(String paymentId) {
        int result = old.rollbackPay(paymentId);
        return result == 1;
    }

    private int mapCurrencyToLegacyCode(String currency) {
        return switch (currency.toUpperCase()) {
            case "KZT" -> 398;
            case "USD" -> 840;
            case "EUR" -> 978;
            default -> 0;
        };
    }

    private String mapLegacyStatusToNewStatus(String legacyStatus) {
        return switch (legacyStatus) {
            case "OK" -> "PAID";
            case "CANCELLED" -> "CANCELLED";
            case "UNKNOWN" -> "NOT_FOUND";
            default -> "PENDING";
        };
    }
}
