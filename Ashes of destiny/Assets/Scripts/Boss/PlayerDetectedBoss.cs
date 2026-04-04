using UnityEngine;

public class PlayerDetectedBoss : MonoBehaviour
{
    bool _canAttackPlayer = false;
    public bool CanAttackPlayer { get => _canAttackPlayer; set => _canAttackPlayer = value; }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _canAttackPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _canAttackPlayer = false;
    }
}
