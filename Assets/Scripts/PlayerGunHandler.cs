using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    private Vector2 mouseScreenPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCommonData.SetMousePosition(mouseScreenPos);
        playerCommonData.SetMouseDir(transform.position);
    }

    public void MousePositionInput(InputAction.CallbackContext context) {
        mouseScreenPos = context.ReadValue<Vector2>();
        Debug.Log("Did mouse pos input thingy");
    }
}
