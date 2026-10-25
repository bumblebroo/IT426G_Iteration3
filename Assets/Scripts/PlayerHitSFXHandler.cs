using UnityEngine;

public class PlayerHitSFXHandler : MonoBehaviour
{
    [SerializeField]
    private SFXHandler sfxHandler;

    [SerializeField]
    private PlayerCommonData playerCommonData;

    [Space]

    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private float volume;

    [SerializeField]
    private float volumeVariation;

    [SerializeField]
    private float pitch;

    [SerializeField]
    private float pitchVariation;

    private void Start() {
        playerCommonData.OnDamage.AddListener(PlaySFX);
    }

    private void OnDestroy() {
        playerCommonData.OnDamage.RemoveListener(PlaySFX);
    }

    private void PlaySFX() {
        sfxHandler.PlaySFX(SFXRequester.PrepareGlobalSFXInfoWithVariation(audioClips, volume, volumeVariation, pitch, pitchVariation));
    }
}
