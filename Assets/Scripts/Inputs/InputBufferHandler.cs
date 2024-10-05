using System.Collections.Generic;
using System.Linq;
using Inputs.Data;
using UnityEngine;

namespace Inputs
{
    /* RequireComponent(typeof(StateMachine))*/
    public class InputBufferHandler : MonoBehaviour
    {
        /*[SerializeField] private StateMachine m_stateMachine = null;*/
        [field: SerializeField] public List<InputData> AvailableInputActions { get; private set; }
        [SerializeField] private bool _showDebugInputs = false;
        
        private List<InputData> _inputBuffer = new();
        
        private void Update()
        {
            ResetInputsState();
            CheckForPlayerInputs();
            ProcessBufferedInputs();
        }

        private void ResetInputsState()
        {
            foreach (var inputAction in AvailableInputActions)
            {
                inputAction.InputEvent.Invoke(false);
            }
        }

        private void ProcessBufferedInputs()
        {
            if (_inputBuffer.Count <= 0) return;

            for (int i = 0; i < _inputBuffer.Count; i++)
            {
                if (Time.time <= _inputBuffer[i].GetTimestamp() /*&& test.CanConsumeInput(m_inputBuffer[i].InputType)*/)
                {
                    if (_showDebugInputs)
                    {
                        print("Consumed input " + _inputBuffer[i].InputType);
                    }
                    
                    _inputBuffer[i].InputEvent.Invoke(true);
                    UnregisterInput(_inputBuffer[i]);
                }
                else if(Time.time > _inputBuffer[i].GetTimestamp())
                {
                    if (_showDebugInputs)
                    {
                        Debug.Log("Unregistered input " + _inputBuffer[i].InputType);
                    }

                    UnregisterInput(_inputBuffer[i]);
                }
            }
        }
        
        private void CheckForPlayerInputs()
        {
            foreach (var keyData in AvailableInputActions.Where(keyData => Input.GetKey(keyData.Key)))
            {
                ValidateAndRegisterInput(keyData.InputType);
            }
        }

        private void ValidateAndRegisterInput(EInputType inputType)
        {
            InputData wantedInput = FindMatchingInput(inputType);

            if (!wantedInput.Equals(default(InputData)))
            {
                RegisterInput(wantedInput);
            }
        }

        private InputData FindMatchingInput(EInputType inputType)
        {
            return AvailableInputActions.FirstOrDefault(input => input.InputType == inputType);
        }

        private void RegisterInput(InputData inputAction)
        {
            if (!_inputBuffer.Contains(inputAction))
            {
                inputAction.RegisterNewTimestamp(Time.time);
                _inputBuffer.Add(inputAction);
            }
        }

        private void UnregisterInput(InputData inputAction)
        {
            if (_inputBuffer.Contains(inputAction))
            {
                _inputBuffer.Remove(inputAction);
            }
        }
    }
}
