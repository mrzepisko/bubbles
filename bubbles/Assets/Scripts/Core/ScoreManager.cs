using Bubbles.Core.Abstract;

namespace Bubbles.Core {
    public class ScoreManager : IScoreManager {
        private const int MaxDistance = 10;
        public int BaseExponent => 1;
        public int MaxExponent => BaseExponent + MaxDistance;
    }
}