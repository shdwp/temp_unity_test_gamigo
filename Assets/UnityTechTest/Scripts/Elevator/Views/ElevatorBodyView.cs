using UnityEngine;

namespace UnityTechTest.Scripts.Elevator.Views
{
    public sealed class ElevatorBodyView : MonoBehaviour
    {
        private ElevatorBodyMediator _mediator;

        [SerializeField] private Light elevatorLight;

        private void Awake()
        {
            _mediator = new ElevatorBodyMediator(this);
        }

        private void OnEnable()
        {
            _mediator.OnEnable();
        }

        private void OnDisable()
        {
            _mediator.OnDisable();
        }

        public void UpdatePosition(float floorProgress)
        {
            var pos = transform.localPosition;
            pos.y = floorProgress;
            transform.localPosition = pos;
            
            elevatorLight.color = Color.red;
        }

        public void ReachedFloor()
        {
            elevatorLight.color = Color.green;
        }
    }
}