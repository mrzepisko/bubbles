namespace Bubbles.Core.Abstract {
    public interface IBubbleSpawner {
        Bubble CreateRandom();
        Bubble Create(IBubbleScore score);
        void Return(Bubble bubble);
    }
}