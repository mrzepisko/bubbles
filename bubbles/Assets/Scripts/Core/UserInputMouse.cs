using System;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public class UserInputMouse : IUserInput, ITickable {
        private const int MouseButtonIndex = 0;
        private readonly Camera camera;

        
        public UserInputMouse(Camera camera) {
            this.camera = camera;
        }
        
        public event Action<Vector3> ButtonDown;
        public event Action<Vector3> ButtonHold;
        public event Action<Vector3> ButtonUp;

        public void Tick() {
            if (Input.GetMouseButtonDown(MouseButtonIndex)) {
                ButtonDown?.Invoke(ScreenToWorld(Input.mousePosition));
            }
            
            if (Input.GetMouseButton(MouseButtonIndex)) {
                ButtonHold?.Invoke(ScreenToWorld(Input.mousePosition));
            }

            if (Input.GetMouseButtonUp(MouseButtonIndex)) {
                ButtonUp?.Invoke(ScreenToWorld(Input.mousePosition));
            }
        }

        Vector3 ScreenToWorld(Vector3 position) {
            return camera.ScreenToWorldPoint(position);
        }
    }
}