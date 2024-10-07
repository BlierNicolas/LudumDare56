using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class VoidEater : MonoBehaviour
{
    [SerializeField] private bool _isBottomVoid;
    private bool _hasSpawnedTetramino = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_hasSpawnedTetramino)
        {
            if (other.TryGetComponent<CharacterStateMachine>(out var state))
            {
                GameManager.Instance.SpawnNextTetramino();
                _hasSpawnedTetramino = true; 
                StartCoroutine(ResetSpawnFlagAfterDelay(2f));
            }
        }
        
        Destroy(other.gameObject);
        
        if (_isBottomVoid)
        {
            SoundManager.Instance.PlayFailureSound();
        }
    }
    private IEnumerator ResetSpawnFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        _hasSpawnedTetramino = false;  
    }
}