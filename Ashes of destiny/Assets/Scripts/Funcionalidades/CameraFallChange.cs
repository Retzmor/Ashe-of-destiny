using UnityEngine;

public class CameraFallChange : MonoBehaviour
{
    [SerializeField] CameraManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
             ChangeCamera();
        }
    }
    public void ChangeCamera()
    {
        manager.CameraFall();
    }
}
