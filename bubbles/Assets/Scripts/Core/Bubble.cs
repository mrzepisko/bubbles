using Bubbles.Config;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public partial class Bubble : MonoBehaviour {
        [Header("Visual")] [SerializeField] private SpriteRenderer sprite;

        
        
        private BubbleScore score;
        private IBubbleMovement movement;
        private IBubbleCollector collector;

        private BubbleConfigItem config;
        private Tile attachedTo;

        public BubbleConfigItem Config => config;
        public Tile AttachedTo => attachedTo;


        private void Awake() {
            movement = GetComponent<IBubbleMovement>();
            collector = GetComponent<IBubbleCollector>();
        }

        public void SetTarget(Tile tile, bool instant = false) {
            attachedTo = tile;
            collector?.InsertInto(tile);

            if (movement != null) {
                if (instant) {
                    movement.Teleport(tile.transform.position);
                } else {
                    movement.MoveTowards(tile.transform.position);
                }
            }
        }

        public void Configure(BubbleConfigItem conf) {
            config = conf;
            sprite.color = conf.Background;
        }

        public void Teleport(Vector3 position) {
            movement.Teleport(position);
        }
    }
}