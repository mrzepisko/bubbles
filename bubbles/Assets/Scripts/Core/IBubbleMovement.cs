using UnityEngine;

namespace Bubbles.Core {
    public interface IBubbleMovement {
        void MoveTowards(Vector3 target);
    }
}