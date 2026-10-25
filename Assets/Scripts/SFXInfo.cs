using UnityEngine;

[System.Serializable]
public class SFXInfo {
    private float volume;
    private float pitch;
    private AudioClip audioClip;
    private Vector2? pos;

    public float ClipDuration => audioClip.length;

    public SFXInfo(AudioClip audioClip, float volume = 1f, float pitch = 1f, Vector2? pos = null) {
        this.volume = Mathf.Clamp01(volume);
        this.pitch = Mathf.Clamp(pitch, -3, 3);
        this.audioClip = audioClip;
        this.pos = pos;
    }

    public void ConfigureAudioSource(AudioSource audioSource) {
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = audioClip;

        if (pos == null) {
            audioSource.spatialBlend = 0;
            return;
        }

        audioSource.spatialBlend = 1;
        audioSource.transform.position = new Vector3(pos.Value.x, pos.Value.y, 0);
    }
}

