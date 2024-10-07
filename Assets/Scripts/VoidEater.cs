using Managers;
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

        if (other.gameObject.layer == 7) //Gary
        {
            GameManager.Instance.ShowLoseScreen();
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