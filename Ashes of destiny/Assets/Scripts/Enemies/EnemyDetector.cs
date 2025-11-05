using UnityEngine;

public class EnemyDetector : Enemies
{
    [SerializeField] LayerMask layerPlayer;
    [SerializeField] float radius;
    [SerializeField] GameObject _player;
    bool _playerDetected = false;

    public bool PlayerDetected { get => _playerDetected; set => _playerDetected = value; }
    public GameObject PlayerPosition { get => _player; set => _player = value; }

    private void Update()
    {
        Collider[] zone = Physics.OverlapSphere(transform.position, radius, layerPlayer);

        if(zone.Length != 0)
        {
            PlayerDetected = true;
        }

        else
        {
            PlayerDetected = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
