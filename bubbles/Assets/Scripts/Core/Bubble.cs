using Bubbles.Config;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    [DefaultExecutionOrder(-1)]
    public partial class Bubble : MonoBehaviour {
        [Header("Visual")] [SerializeField] private SpriteRenderer sprite;

        
        private IBubbleMovement movement;
        private IBubbleCollector collector;
        private IBubbleAnimator animator;
        private IBubbleView view;

        private BubbleConfigItem config;

        public IBubbleMovement Movement => movement;
        public IBubbleAnimator Animator => animator;
        public IBubbleView View => view;


        [Inject]
        private void Construct(IBubbleMovement movement, IBubbleCollector collector, IBubbleAnimator animator, IBubbleView view) {
            this.movement = movement;
            this.collector = collector;
            this.animator = animator;
            this.view = view;
        }

        public void SetTarget(Vector3 position, bool instant = false) {
            //collector?.InsertInto(tile);

            if (movement != null) {
                if (instant) {
                    movement.Teleport(position);
                } else {
                    movement.MoveTowards(position);
                }
            }
        }

        private void SetScore(IBubbleScore score) {
            view.Refresh(score);
        }
    }
}