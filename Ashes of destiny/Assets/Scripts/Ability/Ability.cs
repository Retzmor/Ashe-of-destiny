using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public ParticleSystem selectionParticles;
    public float cooldown;
    public abstract void Execute(AttackPlayer attacker);
}
