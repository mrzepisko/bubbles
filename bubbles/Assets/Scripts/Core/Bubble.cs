using Bubbles.Config;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    [SelectionBase]
    public partial class Bubble : MonoBehaviour {
        [Header("Visual")] [SerializeField] private SpriteRenderer sprite;

        private IBubbleMovement movement;
        private IBubbleAnimator animator;
        private IBubbleView view;
        

        private BubbleConfigItem config;
        private IBubbleScore score;
        
        public IBubbleMovement Movement => movement;
        public IBubbleAnimator Animator => animator;
        public IBubbleView View => view;
        public IBubbleScore Score => score;


        
        [Inject]
        private void Construct(IBubbleMovement movement, IBubbleAnimator animator, IBubbleView view) {
            this.movement = movement;
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
            this.score = score;
            view.Refresh(score);
        }
    }
}