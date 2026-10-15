using UnityEngine;

public class DestroyThisGameObjectOnEvent : MonoBehaviour
{
    public void DestroyThis() {
        Destroy(this.gameObject);
    }
}
