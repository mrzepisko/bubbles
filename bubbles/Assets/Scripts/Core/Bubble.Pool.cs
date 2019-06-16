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
                base.OnDespawned(item);
                item.transform.localPosition = Vector3.zero;
                item.movement.StopMoving();
            }
        }
    }
}