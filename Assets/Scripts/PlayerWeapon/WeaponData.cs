using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private GameObject weaponPrefab;

    public GameObject WeaponPrefab => weaponPrefab;
}
