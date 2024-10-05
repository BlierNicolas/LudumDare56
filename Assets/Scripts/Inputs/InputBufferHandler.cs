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
        [field: SerializeField] public List<InputData> m_availableInputActions { get; private set; }
        [SerializeField] private bool m_showDebugInputs = false;
        
        private List<InputData> m_inputBuffer = new();
        
        private void Update()
        {
            ResetInputsState();
            CheckForPlayerInputs();
            ProcessBufferedInputs();
        }

        private void ResetInputsState()
        {
            foreach (var inputAction in m_availableInputActions)
            {
                inputAction.InputEvent.Invoke(false);
            }
        }

        private void ProcessBufferedInputs()
        {
            if (m_inputBuffer.Count <= 0) return;

            for (int i = 0; i < m_inputBuffer.Count; i++)
            {
                if (Time.time <= m_inputBuffer[i].GetTimestamp() /*&& test.CanConsumeInput(m_inputBuffer[i].InputType)*/)
                {
                    if (m_showDebugInputs)
                    {
                        print("Consumed input " + m_inputBuffer[i].InputType);
                    }
                    
                    m_inputBuffer[i].InputEvent.Invoke(true);
                    UnregisterInput(m_inputBuffer[i]);
                }
                else if(Time.time > m_inputBuffer[i].GetTimestamp())
                {
                    if (m_showDebugInputs)
                    {
                        Debug.Log("Unregistered input " + m_inputBuffer[i].InputType);
                    }

                    UnregisterInput(m_inputBuffer[i]);
                }
            }
        }
        
        private void CheckForPlayerInputs()
        {
            foreach (var keyData in m_availableInputActions.Where(keyData => Input.GetKey(keyData.Key)))
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
            return m_availableInputActions.FirstOrDefault(input => input.InputType == inputType);
        }

        private void RegisterInput(InputData inputAction)
        {
            if (!m_inputBuffer.Contains(inputAction))
            {
                inputAction.RegisterNewTimestamp(Time.time);
                m_inputBuffer.Add(inputAction);
            }
        }

        private void UnregisterInput(InputData inputAction)
        {
            if (m_inputBuffer.Contains(inputAction))
            {
                m_inputBuffer.Remove(inputAction);
            }
        }
    }
}
