using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public GameObject attackPrefab;
    public ParticleSystem hudParticles;
    public ParticleSystem handParticles;
    public float cooldown = 5f;
    public AudioClip attackSound;  
    public AudioClip loopSound;
    public AudioClip abilitySound;
}
