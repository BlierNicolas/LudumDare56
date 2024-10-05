using System;
using UnityEngine;
using UnityEngine.Events;

namespace Inputs.Data
{
    [Serializable]
    public class InputData
    {
        [field: SerializeField] public EInputType InputType { get; private set; } = EInputType.Empty;
        [field: SerializeField] public KeyCode Key { get; private set; } = 0;

        [field: SerializeField] [field: Range(0, 1)] public float ExpirationTime { get; private set; } = 0.0f;
        
        public UnityEvent<bool> InputEvent { get; set; } = new UnityEvent<bool>();
        
        private float m_timeStamp = 0.0f;

        public void RegisterNewTimestamp(float currentTime)
        {
            m_timeStamp = currentTime + ExpirationTime;
        }

        public float GetTimestamp() => m_timeStamp;
    }
}