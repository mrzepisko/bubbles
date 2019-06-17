using UnityEngine;

namespace Bubbles.Core.Abstract {
    public interface IUserInput {
        event System.Action<Vector3> ButtonDown;
        event System.Action<Vector3> ButtonHold;
        event System.Action<Vector3> ButtonUp;

    }
}