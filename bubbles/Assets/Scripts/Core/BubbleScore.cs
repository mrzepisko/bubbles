using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class BubbleScore : IBubbleScore {
        private readonly int exponent;
        
        private long points;

        public BubbleScore(int exponent, int multiplier = 1) {
            this.exponent = exponent;
            points = 2.Pow(exponent) * (long) multiplier;
        }
        
        public int Exponent => exponent;
        public string ValueString => ToString();
        public string PointsString => GetValue(points);

        public override string ToString() {
            return PointsString;
        }

        private string GetValue(long points) {
            return points.ToString("0");
        }

        private string GetSuffix(int exponent) {
            string suffix;
            var check = exponent / 3;
            if (check < 1) {
                suffix = "";
            } else if (check < 2) {
                suffix = "k";
            } else if (check < 3) {
                suffix = "M";
            } else if (check < 4) {
                suffix = "G";
            } else if (check < 5) {
                suffix = "T";
            } else if (check < 6) {
                suffix = "P";
            } else {
                suffix = "*";
            }
            return suffix;
        }
    }
}