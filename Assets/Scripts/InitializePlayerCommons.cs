using UnityEngine;

public class InitializePlayerCommons : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    // Resets after awake but before Start
    private void OnEnable() {
        playerCommonData.Initalize();
    }
}
