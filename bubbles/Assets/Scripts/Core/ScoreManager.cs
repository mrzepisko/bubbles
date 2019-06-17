using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bubbles.Config;
using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class ScoreManager : IScoreManager {
        private const float DelayReturnTime = .2f;
        private const int MaxRowCount = 4;
        private readonly IGridWrapper grid;
        private readonly IBubbleSpawner spawner;
        private readonly IScoreCalculator calculator;
        private readonly IBubbleCollector collector;
        private readonly IBubbleExploder exploder;
        private readonly IGridManager gridManager;
        private readonly ScoreRange scoreRange;

        public ScoreManager(IGridWrapper grid, IBubbleSpawner spawner, IScoreCalculator calculator, 
                IBubbleCollector collector, IBubbleExploder exploder, IGridManager gridManager, ScoreRange scoreRange) {
            this.grid = grid;
            this.spawner = spawner;
            this.calculator = calculator;
            this.collector = collector;
            this.exploder = exploder;
            this.gridManager = gridManager;
            this.scoreRange = scoreRange;
        }

        public void Attached(Bubble bubble) {
            var toJoin = collector.ScoreNeighbours(bubble);
            if (toJoin.Count <= 1) return; //nothing to join
            
            IBubbleScore score = calculator.CalculateScore(bubble.Score, toJoin.Count);
            Tile bestTile = collector.SelectBestTile(toJoin, score);
            
            //detach old bubbles
            foreach (var bb in toJoin) {
                bb.Movement.MoveTowards(bestTile.transform.position);
                bb.StartCoroutine(DelayReturn(bb, DelayReturnTime));
            }

            //create new bubble 
            var newBubble = spawner.Create(score);
            
            #if UNITY_EDITOR
            UnityEditor.EditorGUIUtility.PingObject(newBubble.gameObject);
            #endif
            
            grid.Insert(newBubble, bestTile);

            foreach (var toDrop in FindLooseBubbles(toJoin)) {
                toDrop.Movement.Drop();
                toDrop.StartCoroutine(DelayReturn(toDrop, DelayReturnTime));
            }
            
            Debug.Log($"Collected {newBubble.Score} points");

            if (newBubble.Score.Value > scoreRange.MaxExponent) {
                exploder.Explode(newBubble);
            } else { //continue chain
                Attached(newBubble);
            }

            if (grid.Rows < MaxRowCount) {
                gridManager.InsertNewRow();
            }
        }

        List<Bubble> FindLooseBubbles(HashSet<Bubble> toJoin) {
            List<Bubble> toDrop = new List<Bubble>();
            foreach (var bubble in grid.Bubbles) { //TODO optimize
                if (!IsAttached(bubble)) {
                    toDrop.Add(bubble);
                }
            }

            return toDrop;
        }

        private readonly HexDirection[] looseCheck = {
            HexDirection.E,
            HexDirection.SE,
            HexDirection.SW,
            HexDirection.W,
            HexDirection.NW,
            HexDirection.NE,
        };
        
        bool IsAttached(Bubble bubble, HashSet<Bubble> alreadyChecked = null) {
            if (bubble == null) return false;
            if (alreadyChecked == null) alreadyChecked = new HashSet<Bubble>();
            alreadyChecked.Add(bubble);
            //check self
            var cubeIndex = grid.Get(bubble).Index;
            if (cubeIndex.y == 0) { // verified
                return true;
            }
            //check others
            foreach (var direction in looseCheck) {
                var checkBubble = grid.Neighbour(bubble, direction);
                if (!alreadyChecked.Contains(checkBubble) && IsAttached(checkBubble, alreadyChecked)) {
                    return true;
                }
            }
            return false;
        }

        System.Collections.IEnumerator DelayReturn(Bubble bubble, float time) {
            grid.Detach(bubble);
            yield return new WaitForSeconds(time);
            spawner.Return(bubble);
        }
    }
}