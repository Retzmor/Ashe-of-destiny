using UnityEngine;

public class AddEventEnemy : MonoBehaviour
{
    AttackEnemy attackEnemy;

    private void Start()
    {
        attackEnemy = GetComponent<AttackEnemy>();
    }
    public void AttackAnim()
    {
        attackEnemy.AttackPlayer();
    }
}
