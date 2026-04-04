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
    private bool _hasTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!_hasTriggered && other.CompareTag("Player"))
        {
            _hasTriggered = true;
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
            playerRb.isKinematic = false; // Devolvemos las físicas
        }

        yield return StartCoroutine(Fade(0));

        playerMovement.CanMoving = true;
        Debug.Log("¡QUE COMIENCE LA BATALLA!");
        Destroy(gameObject);
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
}