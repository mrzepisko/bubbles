using UnityEngine;

namespace Bubbles.Config {
    public class ScoreRange {
        private readonly int maxDistance;
        private readonly int explosionDistance;
        private readonly int explosionsPerLevel;
        private int baseExponent;

        public ScoreRange(int baseExponent, int maxDistance, int explosionDistance, int explosionsPerLevel) {
            this.baseExponent = baseExponent;
            this.maxDistance = maxDistance;
            this.explosionDistance = explosionDistance;
            this.explosionsPerLevel = explosionsPerLevel;
        }

        public int BaseExponent => baseExponent;
        public int MaxExponent => BaseExponent + maxDistance;
        public int ExplosionExponent => BaseExponent + explosionDistance;
        public int ExplosionsPerLevel => explosionsPerLevel;
    }
}