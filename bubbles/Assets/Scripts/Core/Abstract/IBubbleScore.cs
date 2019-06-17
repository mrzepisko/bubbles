namespace Bubbles.Core.Abstract {
    public interface IBubbleScore {
        int Exponent { get; }
        string ValueString { get; }
        string PointsString { get; }
    }
}