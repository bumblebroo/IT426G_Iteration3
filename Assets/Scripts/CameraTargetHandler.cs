using UnityEngine;

public class CameraTargetHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private float maxCameraDistance;

    [SerializeField]
    private Transform cameraTarget;
    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = playerCommonData.MousePosition;

        if (playerCommonData.MousePosition.magnitude > maxCameraDistance) {
            targetPos = playerCommonData.MouseDir * maxCameraDistance;
        }

        cameraTarget.position = targetPos;
    }
}
