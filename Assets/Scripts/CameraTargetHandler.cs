using UnityEngine;

public class CameraTargetHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private float percentDistance = 0.3f;

    [SerializeField]
    private float maxCameraDistance;

    [SerializeField]
    private Transform cameraTarget;
    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = (Vector3)playerCommonData.MousePosition - transform.position;
        targetPos *= percentDistance;

        if (targetPos.magnitude > maxCameraDistance) {
            targetPos = playerCommonData.MouseDir * maxCameraDistance;
        }

        cameraTarget.localPosition = targetPos;
    }
}
