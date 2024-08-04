using System.Collections.Generic;
using BNG;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class SecondModeKeyboard: MonoBehaviour
    {   
        //links to letters by lines from the keyboard
        [SerializeField] private List<KeyboardKey> _row1;
        [SerializeField] private List<KeyboardKey> _row2;
        [SerializeField] private List<KeyboardKey> _row3;
        [SerializeField] private List<KeyboardKey> _row4;
        [SerializeField] private PlayerTeleport playerTeleport;

        private const float CURSOR_MOVING_DELAY = 0.25f;
        
        //List of lists for easy navigation
        private List<List<KeyboardKey>> buttonLists; 
        
        //variables responsible for urgency and letter
        private int _currentListIndex; 
        private int _currentButtonIndex;

        //Variable that is responsible for activating left fumstick tracking
        private bool _startTrack;
       
        //variable responsible for the delay between cursor movements
        private bool _canMove = true;
       
        private void Awake()
        {
            buttonLists = new List<List<KeyboardKey>>();
            
            buttonLists.Add(_row1); 
            buttonLists.Add(_row2);
            buttonLists.Add(_row3); 
            buttonLists.Add(_row4);
        }

        //turn on fambstick keyboard controoller
        public void StartFocus()
        {
            _currentListIndex = 0;
            _currentButtonIndex = 0;
            
            _startTrack = true;
            
            SetFocus(_currentListIndex, _currentButtonIndex);
            playerTeleport.enabled = false;
        }
        
        //turn off fambstick keyboard controoller
        public void StopFocus()
        {
            RemoveFocus(_currentListIndex, _currentButtonIndex);

            _startTrack = false;
            playerTeleport.enabled = true;
        }
        
        private void Update()
        {
            if (!_startTrack)
            {
                return;
            }
            
            if (InputBridge.Instance.LeftThumbstickAxis.y > 0.25f)
            {
                MoveFocus(-1, 0); 
            }
            else if (InputBridge.Instance.LeftThumbstickAxis.y < -0.25f )
            {
                MoveFocus(1, 0); 
            }
            else if (InputBridge.Instance.LeftThumbstickAxis.x < -0.25f )
            {
                MoveFocus(0, -1); 
            }
            else if (InputBridge.Instance.LeftThumbstickAxis.x > 0.25f )
            {
                MoveFocus(0, 1); 
            }
            else if(InputBridge.Instance.RightTrigger > 0)
            {
                PressButton();
            }
        }

        private void PressButton()
        {
            if (_canMove)
            {
                buttonLists[_currentListIndex][_currentButtonIndex].SetActive(null);
                
                _canMove = false;
                
                DOVirtual.DelayedCall(CURSOR_MOVING_DELAY, () =>
                {
                    _canMove = true;
                });
            }
        }

        private void MoveFocus(int listChange, int buttonChange)
        {
            if (_canMove)
            {
                RemoveFocus(_currentListIndex, _currentButtonIndex);

                _currentListIndex = Mathf.Clamp(_currentListIndex + listChange, 0, buttonLists.Count - 1);
                _currentButtonIndex = Mathf.Clamp(_currentButtonIndex + buttonChange, 0, buttonLists[_currentListIndex].Count - 1);

                SetFocus(_currentListIndex, _currentButtonIndex);

                _canMove = false;
            
                DOVirtual.DelayedCall(CURSOR_MOVING_DELAY, () =>
                {
                    _canMove = true;
                });
            }
        }

        private void SetFocus(int listIndex, int buttonIndex)
        {
            buttonLists[listIndex][buttonIndex].SetHovering(null);
        }

        private void RemoveFocus(int listIndex, int buttonIndex)
        {
            buttonLists[listIndex][buttonIndex].ResetHovering(null);
        }
    }
}