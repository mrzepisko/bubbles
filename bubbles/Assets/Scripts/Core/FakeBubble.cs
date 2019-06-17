using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public partial class FakeBubble : MonoBehaviour {
        private IBubbleView view;
        private IBubbleMovement movement;

        public IBubbleView View => view;
        public IBubbleMovement Movement => movement;

        //[Inject]
        private void Construct(IBubbleView view, IBubbleMovement movement) {
            this.view = view;
            this.movement = movement;
        }

        public void Pretend(Bubble other) {
            view.Refresh(other.Score);
            movement.Teleport(other.transform.position);
        }
    }
}