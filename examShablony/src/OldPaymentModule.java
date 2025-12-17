import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

public class OldPaymentModule {
    private final Map<String, String> paymentStatuses = new HashMap<>();

    public String makePay(String legacyOrderRef, int amountInCents, int currencyCode) {
        String paymentRef = "LEG-" + UUID.randomUUID();
        paymentStatuses.put(paymentRef, "OK");
        return paymentRef;
    }

    public String checkPay(String legacyPaymentRef) {
        return paymentStatuses.getOrDefault(legacyPaymentRef, "UNKNOWN");
    }

    public int rollbackPay(String legacyPaymentRef) {

        if (paymentStatuses.containsKey(legacyPaymentRef)) {
            paymentStatuses.put(legacyPaymentRef, "CANCELLED");
            return 1;
        }
        return 0;
    }
}
