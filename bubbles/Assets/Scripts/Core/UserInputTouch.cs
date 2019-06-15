using System;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public class UserInputTouch : IUserInput, ITickable {
        private const int TouchIndex = 0;
        
        public event Action<Vector3> ButtonDown;
        public event Action<Vector3> ButtonHold;
        public event Action<Vector3> ButtonUp;

        public void Tick() {
            var touch = Input.GetTouch(TouchIndex);
            
            if (touch.phase == TouchPhase.Began) {
                ButtonDown?.Invoke(touch.position);
            }
            
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                ButtonHold?.Invoke(touch.position);
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
                ButtonUp?.Invoke(Input.mousePosition);
            }
        }
    }
}