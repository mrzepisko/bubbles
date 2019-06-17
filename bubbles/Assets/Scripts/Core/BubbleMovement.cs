using System.Collections;
using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class BubbleMovement : MonoBehaviour, IBubbleMovement {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float dropTime;
        [SerializeField] private float drag;
        [SerializeField] private float explForce;
        [SerializeField] private Vector3 gravity;
        
        private Coroutine moving;
        private Coroutine falling;

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

            if (falling != null) {
                StopCoroutine(falling);
            }
        }

        public void Drop() {
            StopMoving();
            falling = StartCoroutine(DropSmooth());
        }

        IEnumerator MoveTowardsSmooth(Vector3 target) {
            for (;!Mathf.Approximately(Vector3.Distance(target, transform.position), 0);
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime)) {
                yield return null;
            }

            Teleport(target);
        }

        IEnumerator DropSmooth() {
            dropTime = 3f;
            Vector3 velocity = Random.insideUnitCircle * explForce;
            Vector3 gravVelocity = Vector3.zero;
            for (float t = 0; t < dropTime; t += Time.deltaTime) {
                float dt = Time.deltaTime;
                transform.position += (velocity) * dt + gravVelocity; 
                velocity *= (1 - drag);
                gravVelocity += gravity * dt;
                yield return new WaitForSeconds(dt);
            }
        }
    }
}