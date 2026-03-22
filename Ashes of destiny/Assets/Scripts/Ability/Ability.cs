using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public GameObject attackPrefab;
    public int maxAmmo = 10;
    public int currentAmmo;
    public bool isInfinite = false;
    public ParticleSystem hudParticles;
    public ParticleSystem handParticles;
    public float cooldown = 5f;
    public AudioClip attackSound;  
    public AudioClip loopSound;
    public AudioClip abilitySound;

    public void Initialize()
    {
        currentAmmo = maxAmmo;
    }
}
