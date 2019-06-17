
public static class Extensions {
    public static long Pow(this int self, int exponent) {
        long value = 1L;
        for (int i = 0; i < exponent; i++) {
            value *= (long) self;
        }

        return value;
    }
}