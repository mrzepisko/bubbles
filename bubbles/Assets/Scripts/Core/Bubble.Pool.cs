using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public partial class Bubble {
        public class Pool : MonoMemoryPool<IBubbleScore, Bubble> {
            protected override void Reinitialize(IBubbleScore p1, Bubble item) {
                item.SetScore(p1);
            }

            protected override void OnDespawned(Bubble item) {
                item.collider.enabled = false;
                item.transform.localPosition = Vector3.zero;
                item.movement.StopMoving();
                base.OnDespawned(item);
            }

            protected override void OnSpawned(Bubble item) {
                item.collider.enabled = true;
                base.OnSpawned(item);
            }
        }
    }
}