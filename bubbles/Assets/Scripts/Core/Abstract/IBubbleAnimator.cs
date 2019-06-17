namespace Bubbles.Core.Abstract {
    public interface IBubbleAnimator {
        void EnterQueue();
        void LoadedOnCannon();
        void Shoot();
        void Bounce();
    }
}