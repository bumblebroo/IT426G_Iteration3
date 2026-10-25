using UnityEngine;

public class MusicDontDestroyOnLoad : MonoBehaviour
{
    private void Start() {
        if(GameObject.FindGameObjectsWithTag(tag).Length > 1) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
