using Bubbles.Core.Abstract;

namespace Bubbles.Core {
    public class GridManager : IGridManager {
        private const int InitialRows = 4;
        private HexGrid grid;
        private IBubbleSpawner spawner;
        private IGridWrapper gridWrapper;

        public GridManager(HexGrid grid, IBubbleSpawner spawner, IGridWrapper gridWrapper) {
            this.grid = grid;
            this.spawner = spawner;
            this.gridWrapper = gridWrapper;
        }

        public void FillGrid() {
            
            for (int r = 0; r > -InitialRows / 2; r--) {
                FillRow(r, r);
                FillRow(r, r - 1);
            }
        }

        private void FillRow(int q, int r) {
            Tile tile = grid.TileAt(q, r);
            while (tile != null) {
                var bubble = spawner.Create();
                gridWrapper.Insert(bubble, tile);
                tile = grid.Neighbour(tile, HexDirection.E);
            }
        }
    }
}