using UnityEngine;

public class ExplosionGizmo : MonoBehaviour
{
    public float maxRadius = 10f;
    public float expandSpeed = 30f;
    private float currentRadius = 0f;
    void Update()
    {
        currentRadius = Mathf.MoveTowards(currentRadius, maxRadius, expandSpeed * Time.deltaTime);
        transform.localScale = new Vector3(currentRadius * 2, currentRadius * 2, currentRadius * 2);

        if (currentRadius >= maxRadius)
        {
           // Destroy(gameObject, 0.1f);
        }
    }
}
