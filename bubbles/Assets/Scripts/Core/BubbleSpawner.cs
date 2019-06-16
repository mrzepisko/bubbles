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

        public Bubble Create() {
            var value = Random.Range(scoreManager.BaseExponent, scoreManager.MaxExponent);
            var bubble = bubblePool.Spawn(new BubbleScore(value));
            return bubble;
        }
        
    }
}