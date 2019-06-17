using Zenject;

namespace Bubbles.Core {
    public partial class FakeBubble {
        
        public class Pool : MonoMemoryPool<Bubble, FakeBubble> {
            protected override void Reinitialize(Bubble p1, FakeBubble item) {
                item.Pretend(p1);
            }

            protected override void OnDespawned(FakeBubble item) {
                base.OnDespawned(item);
                item.movement.StopMoving();
            }
        }
    }
}