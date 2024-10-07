using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class SpawnOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<CharacterStateMachine>(out var stateMachine))
        {
            Destroy(stateMachine);
            GameManager.Instance.isPlayerActive = false;
            GameManager.Instance.SpawnNextTetramino();
        }
    }
}
