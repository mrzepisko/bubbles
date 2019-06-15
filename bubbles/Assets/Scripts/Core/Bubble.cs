using Bubbles.Config;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public partial class Bubble : MonoBehaviour {
        [Header("Visual")] [SerializeField] private SpriteRenderer sprite;
        
        private IBubbleScore score;
        private IBubbleMovement movement;
        private IBubbleCollector collector;
        
        public void SetTarget(Tile tile) {
            movement.MoveTowards(tile.transform.position);
            collector.InsertInto(tile);
        }

        public void Configure(BubbleConfig.Item conf) {
            sprite.color = conf.Background;
        }
    }
}