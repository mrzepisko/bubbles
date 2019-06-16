namespace Bubbles.Core.Abstract {
    public interface IScoreCalculator {
        IBubbleScore CalculateScore(IBubbleScore score, int count);
    }
}