using UnityEngine;

namespace Bubbles.Config {
    public class ScoreRange {
        private readonly int maxDistance;
        private int baseExponent;
        
        public ScoreRange(int baseExponent, int maxDistance) {
            this.baseExponent = baseExponent;
            this.maxDistance = maxDistance;
        }

        public int BaseExponent => baseExponent;
        public int MaxExponent => BaseExponent + maxDistance;
    }
}