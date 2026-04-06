using System.Collections;
using UnityEngine;
using Zenject;

public class BossAudio : MonoBehaviour
{
    [SerializeField] private AudioClip damageClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip summonClip;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private float loopInterval = 6f;
    private Coroutine _loopCoroutine;
    [Inject] private AudioManager _audioManager;
    public void PlayDamage() => _audioManager.PlaySFX(damageClip, 1f);
    public void PlayDeath() => _audioManager.PlaySFX(deathClip, 1f);
    public void PlayAttack() => _audioManager.PlaySFX(attackClip, 1f);
    public void PlaySummon() => _audioManager.PlaySFX(summonClip, 1f);
    public void PlayFootstep()
    {
        _audioManager.PlaySFX3D(footstepClip, transform.position);
    }

    public void StartBossLoop()
    {
        if (_loopCoroutine == null)
            _loopCoroutine = StartCoroutine(PlayLoopRoutine());
    }

    public void StopBossLoop()
    {
        if (_loopCoroutine != null)
        {
            StopCoroutine(_loopCoroutine);
            _loopCoroutine = null;
        }
    }
    private IEnumerator PlayLoopRoutine()
    {
        while (true)
        {
            float finalWait = loopInterval + Random.Range(-1f, 1f);

            if (summonClip != null)
                _audioManager.PlaySFX(summonClip, 0.8f);

            yield return new WaitForSeconds(finalWait);
        }
    }
}