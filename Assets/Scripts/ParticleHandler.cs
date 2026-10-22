using System;
using UnityEngine;
using UnityEngine.Pool;

public class ParticleHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;

    private ObjectPool<ParticleSystem> objectPool;

    [Space]

    [SerializeField]
    private int objectPoolDefaultCapacity;

    [SerializeField]
    private int objectPoolMaxSize;

    private void Start() {
        objectPool = new ObjectPool<ParticleSystem>(
            createFunc: CreateParticle,
            actionOnGet: OnGetParticle,
            actionOnRelease: OnReleaseParticle,
            actionOnDestroy: OnDestroyParticle,
            collectionCheck: true,
            defaultCapacity: objectPoolDefaultCapacity,
            maxSize: objectPoolMaxSize
        );
    }

    private void OnDestroyParticle(ParticleSystem system) {
        Destroy(system.gameObject);
    }

    private void OnReleaseParticle(ParticleSystem system) {
        system.Stop();
        system.gameObject.SetActive(false);
    }

    private void OnGetParticle(ParticleSystem system) {
        system.gameObject.SetActive(true);
        system.Play();
    }

    private ParticleSystem CreateParticle() {
        return Instantiate(particlePrefab).GetComponent<ParticleSystem>();
    }

    public void SummonParticle(Vector3 pos) {
        ParticleSystem system = objectPool.Get();
        system.transform.position = pos;
    }
}
