using System;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public class UserInputMouse : IUserInput, ITickable {
        private const int MouseButtonIndex = 0;
        
        public event Action<Vector3> ButtonDown;
        public event Action<Vector3> ButtonHold;
        public event Action<Vector3> ButtonUp;

        public void Tick() {
            if (Input.GetMouseButtonDown(MouseButtonIndex)) {
                ButtonDown?.Invoke(Input.mousePosition);
            }
            
            if (Input.GetMouseButton(MouseButtonIndex)) {
                ButtonHold?.Invoke(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(MouseButtonIndex)) {
                ButtonUp?.Invoke(Input.mousePosition);
            }
        }
    }
}