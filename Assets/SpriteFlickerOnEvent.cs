using System.Collections;
using UnityEngine;

public class SpriteFlickerOnEvent : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private float showTime, hideTime;

    [SerializeField]
    private int repeats;

    private bool flashing = false;

    private void Start() {
        playerCommonData.OnDamage.AddListener(StartFlashing);
    }

    private void OnDestroy() {
        playerCommonData.OnDamage.RemoveListener(StartFlashing);
    }

    private void StartFlashing() {
        if (flashing) {
            StopCoroutine(DoFlashes());
        }

        StartCoroutine(DoFlashes());
    }

    private IEnumerator DoFlashes() {
        flashing = true;
        sr.enabled = false;

        for (int i = 0; i < repeats; i++) {
            sr.enabled = false;
            yield return new WaitForSeconds(hideTime);
            sr.enabled = true;
            yield return new WaitForSeconds(showTime);
        }

        sr.enabled = true;
        flashing = false;
    }
}
