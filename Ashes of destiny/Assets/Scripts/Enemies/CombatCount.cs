using System.Collections;
using UnityEngine;

public class CombatCount : MonoBehaviour
{
    [SerializeField] private int targetKills = 5; 
    [SerializeField] private GameObject objectToActivate; 
    [SerializeField] private GameObject particula;
    [SerializeField] private GameObject casa;
    [SerializeField] private GameObject particulaCasa, particulaCasa2, colliderCasa, textFight;
    [SerializeField] private CanvasGroup text;
    private int _currentKills = 0;
    private bool _missionCompleted = false;

    public void RegisterEnemyDeath()
    {
        if (_missionCompleted) return;
        _currentKills++;
        if (_currentKills >= targetKills)
        {
            ActivateSpecialObject();
        }
    }

    private void ActivateSpecialObject()
    {
        if (objectToActivate != null && !objectToActivate.activeSelf)
        {
            objectToActivate.SetActive(true);
            particula.SetActive(true);  
            casa.SetActive(true);
            colliderCasa.SetActive(true);
            textFight.SetActive(false);
            text.alpha = 1;
            StartCoroutine(TextWatch());
        }
    }

    IEnumerator TextWatch()
    {
        yield return new WaitForSeconds(6);
        text.alpha = 0;
    }
}
