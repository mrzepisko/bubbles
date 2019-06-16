using Bubbles.Config;

namespace Bubbles.Core.Abstract {
    public interface IBubbleView {
        void Refresh(IBubbleScore score);
        
        
        BubbleConfigItem Current { get; }
    }
}