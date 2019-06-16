using UnityEngine;

namespace Bubbles.Core.Abstract {
    public interface IBubbleMovement {
        void MoveTowards(Vector3 target);
        void Teleport(Vector3 target);
    }
}