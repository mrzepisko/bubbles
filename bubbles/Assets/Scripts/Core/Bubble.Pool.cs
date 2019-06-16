using Bubbles.Core.Abstract;
using Zenject;

namespace Bubbles.Core {
    public partial class Bubble {
        public class Pool : MonoMemoryPool<BubbleScore, Bubble> {
            protected override void Reinitialize(BubbleScore p1, Bubble item) {
                base.Reinitialize(p1, item);
                item.score = p1;
            }
        }
    }
}