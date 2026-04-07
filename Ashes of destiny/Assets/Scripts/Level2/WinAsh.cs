using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class WinAsh : MonoBehaviour
{
    [Inject] LevelController level;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(LoadCredits), 1f);
        }
    }
     public  void LoadCredits()
    {
        level.WinLevelOne();
    }
}