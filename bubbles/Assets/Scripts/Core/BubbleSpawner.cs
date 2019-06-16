using Bubbles.Config;
using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class BubbleSpawner : IBubbleSpawner {
        private IScoreManager scoreManager;
        private Bubble.Pool bubblePool;
        private BubbleConfig bubbleConfig;

        public BubbleSpawner(IScoreManager scoreManager, Bubble.Pool bubblePool, BubbleConfig bubbleConfig) {
            this.scoreManager = scoreManager;
            this.bubblePool = bubblePool;
            this.bubbleConfig = bubbleConfig;
        }

        public Bubble CreateRandom() {
            var value = Random.Range(scoreManager.BaseExponent, scoreManager.MaxExponent);
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