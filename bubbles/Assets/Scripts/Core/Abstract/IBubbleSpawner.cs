namespace Bubbles.Core.Abstract {
    public interface IBubbleSpawner {
        Bubble CreateRandom();
        Bubble Create(IBubbleScore score);
        //FakeBubble Create(Bubble bubble);
        void Return(Bubble bubble);
    }
}