using UnityEngine;

namespace Bubbles.Config {
    public class ScoreRange {
        private readonly int maxDistance;
        private readonly int explosionDistance;
        private readonly int explosionsPerLevel;

        public ScoreRange(int baseExponent, int maxDistance, int explosionDistance, int explosionsPerLevel) {
            this.BaseExponent = baseExponent;
            this.maxDistance = maxDistance;
            this.explosionDistance = explosionDistance;
            this.explosionsPerLevel = explosionsPerLevel;
        }

        public int BaseExponent { get; set; }

        public int MaxExponent => BaseExponent + maxDistance;
        public int ExplosionExponent => BaseExponent + explosionDistance;
        public int ExplosionsPerLevel => explosionsPerLevel;
    }
}