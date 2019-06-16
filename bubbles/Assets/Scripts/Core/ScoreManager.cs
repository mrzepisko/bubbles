using System.Collections.Generic;
using System.Linq;
using Bubbles.Config;
using Bubbles.Core.Abstract;

namespace Bubbles.Core {
    public class ScoreManager : IScoreManager {

        private readonly IGridWrapper grid;
        private readonly IBubbleSpawner spawner;
        private readonly IScoreCalculator calculator;
        private readonly IBubbleCollector collector;

        public ScoreManager(IGridWrapper grid, IBubbleSpawner spawner, IScoreCalculator calculator, IBubbleCollector collector) {
            this.grid = grid;
            this.spawner = spawner;
            this.calculator = calculator;
            this.collector = collector;
        }

        public void Attached(Bubble bubble) {
            var toJoin = collector.ScoreNeighbours(bubble);
            if (toJoin.Count <= 1) return;
            IBubbleScore score = calculator.CalculateScore(bubble.Score, toJoin.Count);
            Tile bestTile = collector.SelectBestTile(toJoin, score);
            
            //detach old bubbles
            foreach (var bb in toJoin) {
                bb.Movement.MoveTowards(bestTile.transform.position);
                grid.Detach(bb);
                spawner.Return(bb);
            }

            //create new bubble 
            var newBubble = spawner.Create(score);
            #if UNITY_EDITOR
            UnityEditor.EditorGUIUtility.PingObject(newBubble.gameObject);
            #endif
            grid.Insert(newBubble, bestTile);
            
            Attached(newBubble);
        }
        
        
    }
}