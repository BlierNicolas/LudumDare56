using Managers;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 7) //Gary
        {
            GameManager.Instance.ShowWinScreen();
        } 
    }
}
