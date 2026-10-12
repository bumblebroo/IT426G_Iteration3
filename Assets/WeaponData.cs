using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    [Tooltip("Rounds fired per second")]
    private float rps;

    [SerializeField]
    private ProjectileData projectileData;

    public float RPS => rps;
    public ProjectileData ProjectileData => projectileData;
}
