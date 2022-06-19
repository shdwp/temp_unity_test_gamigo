using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTechTest.Scripts.Elevator.Model;

namespace UnityTechTest.Scripts.Elevator.Views
{
    public sealed class ElevatorControlsView : MonoBehaviour
    {
        public event Action<int, Direction> SummonButtonPushed;
        public event Action<int>            FloorButtonPushed;

        // TODO: instantiate dynamically based on motor floor count
        [SerializeField] private Button[] floorButtons;
        [SerializeField] private Button[] summonUpButtons;
        [SerializeField] private Button[] summonDownButtons;

        private ElevatorControlsMediator _mediator;

        private void Awake()
        {
            _mediator = new ElevatorControlsMediator(this);
        }

        private void OnEnable()
        {
            _mediator.OnEnable();

            ButtonsOnClickAddListener(floorButtons, FloorButtonClick);
            ButtonsOnClickAddListener(summonUpButtons, SummonUpButtonClick);
            ButtonsOnClickAddListener(summonDownButtons, SummonDownButtonClick);
        }

        private void OnDisable()
        {
            _mediator.OnDisable();

            ButtonsOnClickRemoveAllListeners(summonDownButtons);
            ButtonsOnClickRemoveAllListeners(summonUpButtons);
            ButtonsOnClickRemoveAllListeners(floorButtons);
        }

        public void ClearFloorButtonRequest(int floor)
        {
            SetButtonColor(floorButtons[InverseButtonIndex(floor)], Color.white);
        }

        public void ClearSummonUpButtonRequest(int floor)
        {
            SetButtonColor(summonUpButtons[InverseButtonIndex(floor)], Color.white);
        }
        
        public void ClearSummonDownButtonRequest(int floor)
        {
            SetButtonColor(summonDownButtons[InverseButtonIndex(floor)], Color.white);
        }

        private void FloorButtonClick(int idx)
        {
            SetButtonColor(floorButtons[idx], Color.green);
            FloorButtonPushed?.Invoke(InverseButtonIndex(idx));
        }

        private void SummonUpButtonClick(int idx)
        {
            SetButtonColor(summonUpButtons[idx], Color.green);
            SummonButtonPushed?.Invoke(InverseButtonIndex(idx), Direction.Up);
        }

        private void SummonDownButtonClick(int idx)
        {
            SetButtonColor(summonDownButtons[idx], Color.green);
            SummonButtonPushed?.Invoke(InverseButtonIndex(idx), Direction.Down);
        }

        private void ButtonsOnClickAddListener(IReadOnlyList<Button> list, Action<int> callback)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var idx = i;
                list[i].onClick.AddListener(() => callback(idx));
            }
        }

        private void ButtonsOnClickRemoveAllListeners(IReadOnlyList<Button> list)
        {
            foreach (var button in list)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        private void SetButtonColor(Button btn, Color color)
        {
            var colors = btn.colors;
            colors.normalColor = color;
            btn.colors = colors;
            btn.image.color = color;
        }

        private int InverseButtonIndex(int index)
        {
            return floorButtons.Length - index - 1;
        }
    }
}