using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SFXRequester : MonoBehaviour
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

    public void PlaySFXGlobal() {
        if (!sfxHandler) return;

        sfxHandler.PlaySFX(PrepareGlobalSFXInfoWithVariation(audioClips, volume, volumeVariation, pitch, pitchVariation));

    }

    public void PlaySFXLocal(Vector3 pos) {
        if (!sfxHandler) return;

        sfxHandler.PlaySFX(PrepareLocalSFXInfoWithVariation(audioClips, volume, volumeVariation, pitch, pitchVariation, pos));
    }

    public static SFXInfo PrepareGlobalSFXInfoWithVariation(AudioClip[] audioClips, float volume, float vVar, float pitch, float pVar) {
        int clipIndex = Random.Range(0, audioClips.Length - 1);
        float sfxVolume = volume + Random.Range(-vVar, vVar);
        float sfxPitch = pitch + Random.Range(-pVar, pVar);

        return new SFXInfo(audioClips[clipIndex], sfxVolume, sfxPitch);
    }

    public static SFXInfo PrepareLocalSFXInfoWithVariation(AudioClip[] audioClips, float volume, float vVar, float pitch, float pVar, Vector3 pos) {
        int clipIndex = Random.Range(0, audioClips.Length - 1);
        float sfxVolume = volume + Random.Range(-vVar, vVar);
        float sfxPitch = pitch + Random.Range(-pVar, pVar);

        return new SFXInfo(audioClips[clipIndex], sfxVolume, sfxPitch, pos);
    }
}
