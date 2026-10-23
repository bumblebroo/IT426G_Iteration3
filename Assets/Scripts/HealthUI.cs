using UnityEngine;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [Space]

    [SerializeField]
    private GameObject iconPrefab;

    [SerializeField]
    private Transform iconParent;

    private List<GameObject> icons = new List<GameObject>();

    private void Start() {
        for (int i = 0; i < playerCommonData.CurrentHealth; i++) {
            GameObject gameObject = Instantiate(iconPrefab, iconParent);
            icons.Add(gameObject);
        }

        playerCommonData.OnDamage.AddListener(UpdateIcons);
    }

    private void OnDestroy() {
        playerCommonData.OnDamage.RemoveListener(UpdateIcons);
    }

    public void UpdateIcons() {
        for (int i = 0; i < icons.Count; i++) {
            icons[i].SetActive(i <= (playerCommonData.CurrentHealth - 1));
        }
    }
}
