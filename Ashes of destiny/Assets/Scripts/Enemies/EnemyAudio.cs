using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyAudio : MonoBehaviour
{
    [Inject] AudioManager audioManager;

    [SerializeField] AudioClip[] idleSounds;
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
}
