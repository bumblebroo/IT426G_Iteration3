using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SFXHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int defaultCapacity;

    [SerializeField]
    private int maxSize;

    private ObjectPool<AudioSource> objectPool;

    private void Start() {
        objectPool = new ObjectPool<AudioSource>(
            createFunc: Create,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyAudioSource,
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    #region ObjectPool functions
    private AudioSource Create() {
        return Instantiate(prefab).GetComponent<AudioSource>();
    }

    private void OnGet(AudioSource audioSource) {
        audioSource.gameObject.SetActive(true);
    }

    private void OnRelease(AudioSource audioSource) {
        audioSource.Stop();
        audioSource.gameObject.SetActive(false);
    }

    private void OnDestroyAudioSource(AudioSource audioSource) {
        Destroy(audioSource);
    }

    private IEnumerator ReturnAudioSource(AudioSource audioSource, float seconds) {
        yield return new WaitForSeconds(seconds + 1);
        objectPool.Release(audioSource);
    }
    #endregion

    public void PlaySFX(SFXInfo sfxInfo) {
        AudioSource audioSource = objectPool.Get();
        sfxInfo.ConfigureAudioSource(ref audioSource);
        audioSource.Play();
        StartCoroutine(ReturnAudioSource(audioSource, sfxInfo.ClipDuration));
    }
}

