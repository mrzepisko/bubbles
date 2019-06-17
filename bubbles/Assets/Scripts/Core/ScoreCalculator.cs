using Bubbles.Config;
using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class ScoreCalculator : IScoreCalculator {
        private const int IncrementBaseEveryLevels = 3;
        private readonly ScoreRange scoreRange;
        private readonly IDataManager dataManager;

        private PlayerData data;
        private long pointsCurrentLevel;
        private long pointsToNextLevel;

        public float LevelProgress => Mathf.InverseLerp(pointsCurrentLevel, pointsToNextLevel, data.Points);

        public ScoreCalculator(ScoreRange scoreRange, IDataManager dataManager) {
            this.scoreRange = scoreRange;
            this.dataManager = dataManager;

            data = dataManager.Load();
            pointsToNextLevel = PointsToReach(data.Level + 1);
            pointsCurrentLevel = PointsToReach(data.Level);
        }

        public IBubbleScore CalculateScore(IBubbleScore score, int count) {
            var expScore = score.Exponent + count - 1;
            var exponent = expScore;
            var points = 2.Pow(exponent) * data.Multiplier;
            data.Points += points;
            while (pointsToNextLevel < data.Points) {
                pointsCurrentLevel = pointsToNextLevel;
                data.Level++;
                scoreRange.BaseExponent = data.Level / IncrementBaseEveryLevels;
                pointsToNextLevel = PointsToReach(data.Level);
            }
            dataManager.Save(data);
            return new BubbleScore(expScore, data.Multiplier);
        }

        long PointsToReach(int level) {
            long first = 2.Pow(scoreRange.BaseExponent) * scoreRange.ExplosionsPerLevel;
            long last = 2.Pow(scoreRange.BaseExponent + scoreRange.ExplosionExponent) * scoreRange.ExplosionsPerLevel;
            long total = ((first + last) / 2L) * (long) level;
            return total;
        }
    }
}