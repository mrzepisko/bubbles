using System;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public class UserInputTouch : IUserInput, ITickable {
        private const int TouchIndex = 0;
        private readonly Camera camera;

        public UserInputTouch(Camera camera) {
            this.camera = camera;
        }
        
        public event Action<Vector3> ButtonDown;
        public event Action<Vector3> ButtonHold;
        public event Action<Vector3> ButtonUp;

        public void Tick() {
            var touch = Input.GetTouch(TouchIndex);
            
            if (touch.phase == TouchPhase.Began) {
                ButtonDown?.Invoke(ScreenToWorld(touch.position));
            }
            
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                ButtonHold?.Invoke(ScreenToWorld(touch.position));
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
                ButtonUp?.Invoke(ScreenToWorld(Input.mousePosition));
            }
        }
        
        Vector3 ScreenToWorld(Vector3 position) {
            return camera.ScreenToWorldPoint(position);
        }
    }
}