using System.Collections.Generic;
using Bubbles.Core.Abstract;
using JetBrains.Annotations;

namespace Bubbles.Core {

    public class GridWrapper : IGridWrapper { //TODO delegate methods from HexGrid
        private Dictionary<Tile, Bubble> t2b;
        private Dictionary<Bubble, Tile> b2t;

        private HexGrid grid;
        
        public Tile Get(Bubble bubble) => Get(b2t, bubble);
        public Bubble Get(Tile tile) => Get(t2b, tile);

        public GridWrapper(HexGrid grid) {
            this.grid = grid;
            t2b = new Dictionary<Tile, Bubble>();
            b2t = new Dictionary<Bubble, Tile>();
        }

        public V Get<K, V>(Dictionary<K, V> from, K key) where K : class where V : class {
            if (key != null && from.TryGetValue(key, out var value)) {
                return value;
            } else {
                return null;
            }
        }

        public bool Insert(Bubble bubble, Tile tile) { //instant
            if (QueryMaps(bubble, tile)) {
                bubble.SetTarget(tile.transform.position, true);
                return true;
            }
            return false;
        }
        public bool Attach(Bubble bubble, Tile tile) { //move towards
            if (QueryMaps(bubble, tile)) {
                bubble.SetTarget(tile.transform.position);
                return true;
            }

            return false;
        }

        private bool QueryMaps(Bubble bubble, Tile tile) {
            if (t2b.ContainsKey(tile)) {
                return false; //cannot insert, tile taken
            }

            //free old tile
            Detach(bubble);

            b2t.Add(bubble, tile);
            t2b.Add(tile, bubble);
            return true;
        }

        public void Detach(Bubble bubble) {
            if (b2t.TryGetValue(bubble, out var tile)) {
                b2t.Remove(bubble);
                t2b.Remove(tile);
            }
        }

        public Tile TileAt(int x, int z) {
            return grid.TileAt(x, z);
        }

        public List<Tile> Neighbours(Tile tile) {
            return grid.Neighbours(tile);
        }

        public Tile Neighbour(Tile tile, HexDirection direction) {
            return grid.Neighbour(tile, direction);
        }

        public List<Tile> TilesInRange(Tile center, int range) {
            return grid.TilesInRange(center, range);
        }

        public List<Bubble> Neighbours(Bubble bubble) {
            List<Bubble> list = new List<Bubble>();
            Tile fromTile = Get(bubble);
            if (fromTile == null) {
                return list;
            }

            var tiles = Neighbours(fromTile);
            foreach (var tile in tiles) {
                var bb = Get(tile);
                if (bb != null) {
                    list.Add(bb);
                }
            }

            return list;
        }
    }
}