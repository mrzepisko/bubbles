using System.Collections.Generic;
using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class GridManager : IGridManager {
        private const int InitialRows = 4;
        private const int RowWidth = 12;


        private readonly IBubbleSpawner spawner;
        private readonly IGridWrapper grid;
        
        public GridManager(IBubbleSpawner spawner, IGridWrapper grid) {
            this.spawner = spawner;
            this.grid = grid;
        }

        public void FillGrid() {
            for (int r = 0; r > -InitialRows / 2; r--) {
                FillRow(r, r);
                FillRow(r, r - 1);
            }
        }

        private void FillRow(int q, int r, Vector3 offset = default) {
            Tile tile = grid.TileAt(q, r);
            while (tile != null) {
                var bubble = spawner.CreateRandom();
                bubble.Movement.Teleport(tile.transform.position + offset);
                grid.Attach(bubble, tile);
                tile = grid.Neighbour(tile, HexDirection.E);
            }
        }

        public void InsertNewRow() {
//            var row = PrepareRow();
            grid.MoveRows(1);
                FillRow(0, 0, Vector3.up * 1.7f);
                FillRow(0, -1, Vector3.up * 1.7f);
        }
        
        private List<Bubble> PrepareRow() {
            List<Bubble> newRow = new List<Bubble>();
            for (int i = 0; i < RowWidth; i++) {
                newRow.Add(spawner.CreateRandom());
            }

            return newRow;
        }
        
    }
}