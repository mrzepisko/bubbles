using Bubbles.Core.Abstract;

namespace Bubbles.Core {
    public class ScoreCalculator : IScoreCalculator {
        public IBubbleScore CalculateScore(IBubbleScore score, int count) {
            return new BubbleScore(score.Value + count - 1);
        }
    }
}