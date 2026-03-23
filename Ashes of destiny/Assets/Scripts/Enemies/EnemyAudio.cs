using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyAudio : MonoBehaviour
{
    [Inject] AudioManager audioManager;
    [SerializeField] AudioClip[] idleSounds;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    void Start()
    {
        StartCoroutine(RandomRoar());
    }
    IEnumerator RandomRoar()
    {
        while (true)
        {
            float wait = Random.Range(5f, 12f);

            yield return new WaitForSeconds(wait);

            AudioClip clip = idleSounds[Random.Range(0, idleSounds.Length)];

            audioManager.PlaySFX3D(clip, transform.position);
        }
    }

    public void DamageEnemy()
    {
        audioManager.PlaySFX3D(idleSounds[0], transform.position);
    }

    public void DamageAttack()
    {
        audioManager.PlaySFX(attackSound, 1f);
    }

    public void DamageHit()
    {
        audioManager.PlaySFX(hitSound, 1f);
    }

    public void DamageDeath()
    {
        audioManager.PlaySFX(deathSound, 1f);
    }
}
