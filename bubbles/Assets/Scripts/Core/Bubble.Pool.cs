using Bubbles.Core.Abstract;
using Zenject;

namespace Bubbles.Core {
    public partial class Bubble {
        public class Pool : MonoMemoryPool<IBubbleScore, Bubble> {
            protected override void Reinitialize(IBubbleScore p1, Bubble item) {
                item.SetScore(p1);
            }
        }
    }
}