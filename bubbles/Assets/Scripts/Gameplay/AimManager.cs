using Bubbles.Core;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class AimManager : MonoBehaviour {
        private const string BubbleTag = "Bubble";
        [SerializeField] private float minY = 1f;
        [SerializeField] private Transform shootFrom;
        
        private IBubbleCannon cannon;
        private IUserInput input;
        private HexGrid grid;
        private IGridWrapper gridWrapper;

        private Vector3[] steps;
        private Tile futureTile;
        
        public Vector3[] Steps => steps;
        public Tile FutureTile => futureTile;

        [Inject]
        private void Construct(IBubbleCannon cannon, IUserInput input, HexGrid grid, IGridWrapper gridWrapper) {
            this.cannon = cannon;
            this.input = input;
            this.grid = grid;
            this.gridWrapper = gridWrapper;
            steps = new Vector3[3];
        }

        private void OnEnable() {
            input.ButtonHold += InputOnButtonHold;
            input.ButtonUp += InputOnButtonUp;
            input.ButtonDown += InputOnButtonDown;
        }

        private void OnDisable() {
            input.ButtonHold -= InputOnButtonHold;
            input.ButtonUp -= InputOnButtonUp;
            input.ButtonDown -= InputOnButtonDown;
        }
        

        private void InputOnButtonDown(Vector3 position) {
            futureTile = FindTile(position);
        }

        private void InputOnButtonHold(Vector3 position) {
            futureTile = FindTile(position);
        }

        private void InputOnButtonUp(Vector3 position) {
            var tile = FindTile(position);
            if (tile != null) {
                cannon.ShootAt(tile);
            }

            futureTile = null;
        }

        private Vector3 CalculateDirection(Vector3 position) {
            var direction = Vector3.ProjectOnPlane(position - shootFrom.position, shootFrom.forward).normalized;
            return direction;
        }

        //TODO make it shorter
        private Tile FindTile(Vector3 position) {
            var direction = CalculateDirection(position);
            RaycastHit hit;
            if (Physics.Raycast(shootFrom.position, direction, out hit, 100f)) {
                //aimed to low
                if (hit.point.y - shootFrom.position.y < minY) {
                    steps[0] = steps[1] = steps[2] = shootFrom.position;
                    return null;
                }
                steps[0] = shootFrom.position;
                steps[1] = hit.point;
                
                if (hit.transform.CompareTag(BubbleTag)) {
                    steps[2] = steps[1];
                    return SelectNeighbour(hit);
                } else {
                    RaycastHit hit2nd;
                    var newDirection = direction;
                    newDirection.x = -newDirection.x;
                    if (Physics.Raycast(hit.point, newDirection, out hit2nd, 100f)) {
                        steps[2] = hit2nd.point;
                        return SelectNeighbour(hit2nd);
                    } else {
                        steps[2] = steps[1]; // shouldn't
                        return null;
                    }
                }
                
            }

            return null;
        }

        private Tile SelectNeighbour(RaycastHit hit) {
            var offset = (hit.point - hit.transform.position).x;
            var dir = offset < 0 ? HexDirection.SE : HexDirection.SW;
            var bubble = hit.transform.GetComponentInParent<Bubble>();
            var tileHit = gridWrapper.Get(bubble);
            return grid.Neighbour(tileHit, dir);
        }

        private void OnDrawGizmosSelected() {
            var minAim = shootFrom.position + Vector3.up * minY;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(minAim + Vector3.right * 10f, minAim - Vector3.right * 10f);
        }
    }
}