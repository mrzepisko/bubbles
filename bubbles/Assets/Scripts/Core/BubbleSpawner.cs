using Bubbles.Config;
using UnityEngine;

namespace Bubbles.Core {
    public class BubbleSpawner {
        private IScoreManager scoreManager;
        private Bubble.Pool bubblePool;
        private BubbleConfig bubbleConfig;

        Bubble Create() {
            var bubble = bubblePool.Spawn();
            var value = Random.Range(scoreManager.BaseExponent, scoreManager.MaxExponent);
            var config = bubbleConfig.Get(value);
            bubble.Configure(config);
            return bubble;
        }
        
    }
}