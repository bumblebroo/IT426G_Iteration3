using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private string deathSceneName;

    private void Start() {
        playerCommonData.OnDeath.AddListener(Died);
    }

    private void OnDestroy() {
        playerCommonData.OnDeath.RemoveListener(Died);
    }

    public void Died() {
        SceneManager.LoadScene(deathSceneName);
    }
}
