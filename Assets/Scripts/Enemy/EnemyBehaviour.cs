using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    protected PlayerCommonData playerCommonData;

    public virtual void Initalize(PlayerCommonData playerCommonData) {
        this.playerCommonData = playerCommonData;
    }
}
