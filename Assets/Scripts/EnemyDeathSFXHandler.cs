using UnityEngine;

public class EnemyDeathSFXHandler : MonoBehaviour
{
    [SerializeField]
    private SFXHandler sfxHandler;

    [Space]

    [SerializeField]
    private float volume;

    [SerializeField]
    private float volumeVariation;

    [SerializeField]
    private float pitch;

    [SerializeField]
    private float pitchVariation;

    [SerializeField]
    private AudioClip[] audioClips;

    public void PlaySFX(Vector3 pos) {
        if (!sfxHandler) return;

        int clipIndex = Random.Range(0, audioClips.Length - 1);
        float sfxVolume = volume + Random.Range(-volumeVariation, volumeVariation);
        float sfxPitch = pitch + Random.Range(-pitchVariation, pitchVariation);

        sfxHandler.PlaySFX(new SFXInfo(audioClips[clipIndex], sfxVolume, sfxPitch, pos));
    }
}
