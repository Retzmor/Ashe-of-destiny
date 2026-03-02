using UnityEngine;

public class WorldCrossHairController : MonoBehaviour
{
    [SerializeField] RectTransform crossHairUI;
    [SerializeField] Camera aimCamera;
    [SerializeField] float maxDistance = 20f;
    [SerializeField] float crossHairOffSetMultiplier = 0.01f;
    [SerializeField] LayerMask raycastMask;

    public Vector3 CurrentAimPoint { get; private set; }
    void Start()
    {
        Debug.Log(gameObject.name);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = aimCamera.ScreenPointToRay(screenCenter);
        Vector3 targetPos;

        if(Physics.Raycast(ray, out RaycastHit hit, maxDistance, raycastMask))
        {
            targetPos = hit.point + hit.normal * crossHairOffSetMultiplier;
            crossHairUI.rotation = Quaternion.LookRotation(hit.normal);
        }

        else
        {
            targetPos = ray.GetPoint(maxDistance);
            crossHairUI.forward = aimCamera.transform.forward;
        }
        crossHairUI.transform.position = targetPos;
        CurrentAimPoint = targetPos;
    }
}
