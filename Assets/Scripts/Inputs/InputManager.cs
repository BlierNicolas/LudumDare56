using System.Collections.Generic;
using Inputs.Data;
using UnityEngine;

namespace Inputs
{
    [RequireComponent(typeof(InputBufferHandler))]
    public class InputManager : MonoBehaviour
    {
        // Singleton
        public static InputManager Instance { get; private set; }
        
        private Dictionary<EInputType, bool> m_inputStates = new();
        private InputBufferHandler m_inputBuffer = null;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            m_inputBuffer ??= GetComponent<InputBufferHandler>();

            LinkInputEvents();
        }
        
        private void LinkInputEvents()
        {
            foreach (var inputData in m_inputBuffer.m_availableInputActions)
            {
                inputData.InputEvent.AddListener((value) => m_inputStates[inputData.InputType] = value);
            }
        }

        public bool GetInput(EInputType inputType) => m_inputStates.ContainsKey(inputType) && m_inputStates[inputType];
    }
}

