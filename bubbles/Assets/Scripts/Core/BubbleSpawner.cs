using Bubbles.Config;
using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class BubbleSpawner : IBubbleSpawner {
        private Bubble.Pool bubblePool;
        private ScoreRange scoreRange;
        
        public BubbleSpawner(Bubble.Pool bubblePool, ScoreRange scoreRange) {
            this.bubblePool = bubblePool;
            this.scoreRange = scoreRange;
        }

        public Bubble CreateRandom() {
            var value = Random.Range(scoreRange.BaseExponent, scoreRange.MaxExponent);
            return Create(new BubbleScore(value));
        }

        public Bubble Create(IBubbleScore score) {
            var bubble = bubblePool.Spawn(score);
            return bubble;
        }

        public void Return(Bubble bubble) {
            bubblePool.Despawn(bubble);
        }
    }
}