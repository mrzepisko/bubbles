using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class BubbleScore : IBubbleScore {
        private int exponent;

        public BubbleScore(int exponent) {
            this.exponent = exponent;
        }
        
        public int Value => exponent;
        public string ValueString => ToString();

        public override string ToString() {
            int exp = exponent % 10;
            var val = Mathf.Pow(2, exp);
            string suffix;
            if (exponent < 10) {
                suffix = "";
            } else if (exponent < 20) {
                suffix = "k";
            } else if (exponent < 30) {
                suffix = "M";
            } else if (exponent < 40) {
                suffix = "G";
            } else if (exponent < 50) {
                suffix = "T";
            } else if (exponent < 60) {
                suffix = "P";
            } else {
                suffix = "*";
            }

            return $"{val:0}{suffix}";
        }
    }
}