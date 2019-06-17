using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bubbles.Core.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace Bubbles.Core {
    public class GridWrapper : IGridWrapper {
        private const float RowHeight = 1.7f;
        private Dictionary<Tile, Bubble> t2b;
        private Dictionary<Bubble, Tile> b2t;

        private HexGrid grid;
        private Camera camera;

        public Tile Get(Bubble bubble) => Get(b2t, bubble);
        public Bubble Get(Tile tile) => Get(t2b, tile);

        public GridWrapper(HexGrid grid, Camera camera) {
            this.grid = grid;
            this.camera = camera;
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

        public bool Insert(Bubble bubble, Tile tile) {
            //instant
            if (QueryMaps(bubble, tile)) {
                bubble.SetTarget(tile.transform.position, true);
                return true;
            }

            return false;
        }

        public bool Attach(Bubble bubble, Tile tile) {
            //move towards
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

        public Tile Nearest(Vector3 position) {
            var projected = Vector3.ProjectOnPlane(position, grid.transform.up);
            return grid.Tiles.Values.OrderBy(t => (t.transform.position - projected).sqrMagnitude).First();
        }

        public List<Bubble> Neighbours(Tile tile) {
            List<Bubble> list = new List<Bubble>();
            var tiles = grid.Neighbours(tile);
            foreach (var t in tiles) {
                var bb = Get(t);
                if (bb != null) {
                    list.Add(bb);
                }
            }

            return list;
        }

        public Tile Neighbour(Tile tile, HexDirection direction) {
            return grid.Neighbour(tile, direction);
        }


        public Bubble Neighbour(Bubble bubble, HexDirection direction) {
            var tile = Neighbour(Get(bubble), direction);
            return Get(tile);
        }

        public List<Tile> TilesInRange(Tile center, int range) {
            return grid.TilesInRange(center, range);
        }

        public List<Bubble> BubblesInRange(Bubble center, int range) {
            var tiles = grid.TilesInRange(Get(center), range);
            return BubblesToTiles(tiles);
        }

        private List<Tile> BubblesToTiles(List<Bubble> bubbles) {
            return bubbles.Select(b => Get(b)).Where(t => t != null).ToList();
        }

        private List<Bubble> BubblesToTiles(List<Tile> tiles) {
            return tiles.Select(t => Get(t)).Where(b => b != null).ToList();
        }

        public List<Bubble> Neighbours(Bubble bubble) {
            Tile fromTile = Get(bubble);
            if (fromTile == null) {
                return new List<Bubble>(); //empty
            }

            return Neighbours(fromTile);
        }

        public void MoveRows(int count) {
            Dictionary<Bubble, Tile> tmp = new Dictionary<Bubble, Tile>();
            CubeIndex down = new CubeIndex(-1 * count, 2 * count, -1 * count);
            //sort by row inverted
            foreach (var tile in Tiles) {
                var bubble = Get(tile);
                var newTile = grid.TileAt(tile.Index + down);
                tmp.Add(bubble, newTile);
            }

            b2t.Clear();
            t2b.Clear();

            foreach (var bubble in tmp.Keys) {
                var tile = tmp[bubble];
                Attach(bubble, tile);
            }
        }

        public List<Tile> Tiles => t2b.Keys.ToList();
        public List<Bubble> Bubbles => b2t.Keys.ToList();
        public int Rows => Tiles.Max(t => t.Index.y);
    }
}