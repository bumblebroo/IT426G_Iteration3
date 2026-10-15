using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    public Sprite Sprite => sprite;
    public float Speed => speed;
    public int Damage => damage;
}
