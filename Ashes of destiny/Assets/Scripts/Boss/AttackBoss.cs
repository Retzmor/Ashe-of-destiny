using UnityEngine;

public class AttackBoss : MonoBehaviour
{
    BossController bossController;
    [SerializeField] PlayerDetectedBoss boss;
    [SerializeField] HealthPlayer player;
    [SerializeField] float damage;

    private void Start()
    {
        bossController = GetComponent<BossController>();
    }

    public void AttackPlayer()
    {
        if(boss.CanAttackPlayer == true)
        {
            player.ChangeHealth(damage, transform.position);
        }
    }
}
