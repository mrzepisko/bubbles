namespace Bubbles.Core {
    public struct BubbleScore {
        private int exponent;

        public BubbleScore(int exponent) {
            this.exponent = exponent;
        }


        public int Exponent => exponent;
    }
}