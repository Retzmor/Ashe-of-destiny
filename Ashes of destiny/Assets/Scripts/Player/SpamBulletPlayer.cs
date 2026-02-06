using UnityEngine;
using Zenject.Asteroids;

public class SpamBulletPlayer : MonoBehaviour
{
    [SerializeField] Transform cam;
    private void FixedUpdate()
    {
        //transform.rotation = cam.rotation;
       // transform.LookAt(cam);
    }

    public void ChangeRotation(Vector3 rotation)
    {
        transform.Rotate(rotation);
        Debug.Log(rotation);
    }
}
