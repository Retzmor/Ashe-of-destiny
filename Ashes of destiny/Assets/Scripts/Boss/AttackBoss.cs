using UnityEngine;

public class AttackBoss : MonoBehaviour
{
    BossController bossController;
    [SerializeField] PlayerDetectedBoss boss;
    [SerializeField] HealthPlayer player;
    [SerializeField] float damage;
    BossAudio bossAudio;

    private void Start()
    {
        bossController = GetComponent<BossController>();
        bossAudio = GetComponent<BossAudio>();
    }

    public void AttackPlayer()
    {
        if(boss.CanAttackPlayer == true)
        {
            player.ChangeHealth(damage, transform.position);
            bossAudio.PlayAttack();
        }
    }
}
