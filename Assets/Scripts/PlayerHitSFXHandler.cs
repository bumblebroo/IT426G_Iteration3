using UnityEngine;

public class PlayerHitSFXHandler : SFXRequester
{
    [SerializeField]
    private PlayerCommonData playerCommonData;


    private void Start() {
        playerCommonData.OnDamage.AddListener(PlaySFXGlobal);
    }

    private void OnDestroy() {
        playerCommonData.OnDamage.RemoveListener(PlaySFXGlobal);
    }
}
