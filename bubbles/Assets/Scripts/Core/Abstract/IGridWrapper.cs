namespace Bubbles.Core.Abstract {
    public interface IGridWrapper {
        Tile Get(Bubble bubble);
        Bubble Get(Tile tile);
        bool Attach(Bubble bubble, Tile tile);
        bool Insert(Bubble bubble, Tile tile);
        void Detach(Bubble bubble);
    }
}