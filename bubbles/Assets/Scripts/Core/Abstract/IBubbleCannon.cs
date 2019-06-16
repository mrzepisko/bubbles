namespace Bubbles.Core.Abstract {
    public interface IBubbleCannon {
        void ShootAt(Tile tile);
        Bubble Peek();
    }
}