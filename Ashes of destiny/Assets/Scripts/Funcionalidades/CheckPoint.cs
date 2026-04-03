using UnityEngine;
using Zenject;

public class CheckPoint : MonoBehaviour
{
    [Inject] LevelController levelController;
    private bool _activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_activated)
        {
            _activated = true;
            levelController.SetCheckpoint(transform.position);
        }
    }
}
