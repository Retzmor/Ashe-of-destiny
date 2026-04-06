using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Necesario para el Image de la pantalla negra

public class BossArenaTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bossGo;
    [SerializeField] private Transform playerArenaPoint;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CanvasGroup blackScreenGroup;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private GameObject rock1;
    [SerializeField] private GameObject rock2;
    [SerializeField] private HealthBoss healthBoss;
    [SerializeField] GameObject barraVidaBoss;
    private Transform bossPosition;
    private bool _hasTriggered = false;

    private void Start()
    {
        bossPosition = bossGo.transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TransitionToBoss());
        }
    }
    private IEnumerator TransitionToBoss()
    {
        playerMovement.CanMoving = false;

        Rigidbody playerRb = playerMovement.GetComponent<Rigidbody>();

        yield return StartCoroutine(Fade(1));

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            playerRb.isKinematic = true;
            playerRb.position = playerArenaPoint.position;
            playerMovement.transform.rotation = playerArenaPoint.rotation;
        }

        if (bossGo != null) bossGo.SetActive(true);
        rock1.SetActive(true);
        rock2.SetActive(true);

        yield return new WaitForSeconds(1f);

        if (playerRb != null)
        {
            playerRb.isKinematic = false; 
        }

        yield return StartCoroutine(Fade(0));
        barraVidaBoss.SetActive(true);
        playerMovement.CanMoving = true;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = blackScreenGroup.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            blackScreenGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }
        blackScreenGroup.alpha = targetAlpha;
    }

    public void ResetBoss()
    {
         bossGo.transform.position = bossPosition.position;
        if (healthBoss.gameObject.activeSelf)
         healthBoss.ResetHealth();
        rock1.SetActive(false);
        rock2.SetActive(false);
        bossGo.gameObject.SetActive(false);
    }
}