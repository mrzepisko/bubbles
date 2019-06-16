using System.Collections;
using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class BubbleMovement : MonoBehaviour, IBubbleMovement {
        [SerializeField] private float moveSpeed;
        private Coroutine moving;

        public void MoveTowards(Vector3 target) {
            StopMoving();
            moving = StartCoroutine(MoveTowardsSmooth(target));
        }

        public void MoveTowards(Vector3 from, Vector3 to) {
            Teleport(from);
            MoveTowards(to);
        }

        public void Teleport(Vector3 target) {
            StopMoving();
            transform.position = target;
        }

        public void StopMoving() {
            if (moving != null) {
                StopCoroutine(moving);
            }
        }

        IEnumerator MoveTowardsSmooth(Vector3 target) {
            for (;!Mathf.Approximately(Vector3.Distance(target, transform.position), 0);
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime)) {
                yield return null;
            }

            Teleport(target);
        }
    }
}