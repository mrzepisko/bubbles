using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Gameplay {
    public class BubbleAnimator : MonoBehaviour, IBubbleAnimator {
        [SerializeField] private Animator animator;

        [Header("Triggers")]
        [SerializeField] private string enterQueue;

        [SerializeField] private string loadCannon;
        [SerializeField] private string shoot;
        [SerializeField] private string bounce;

        public void EnterQueue() {
            animator.SetTrigger(enterQueue);
        }

        public void LoadedOnCannon() {
            animator.SetTrigger(loadCannon);
        }

        public void Shoot() {
            animator.SetTrigger(shoot);
        }

        public void Bounce() {
            animator.SetTrigger(bounce);
        }
    }
}