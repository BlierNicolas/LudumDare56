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
        
        private Dictionary<EInputType, bool> _inputStates = new();
        private InputBufferHandler _inputBuffer = null;
        
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
            _inputBuffer ??= GetComponent<InputBufferHandler>();

            LinkInputEvents();
        }
        
        private void LinkInputEvents()
        {
            foreach (var inputData in _inputBuffer.AvailableInputActions)
            {
                inputData.InputEvent.AddListener((value) => _inputStates[inputData.InputType] = value);
            }
        }

        public bool GetInput(EInputType inputType) => _inputStates.ContainsKey(inputType) && _inputStates[inputType];
    }
}

