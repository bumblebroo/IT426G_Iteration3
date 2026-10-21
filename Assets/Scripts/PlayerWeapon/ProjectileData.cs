using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private int damage = 1;

    public Sprite Sprite => sprite;
    public float Speed => speed;
    public int Damage => damage;
}
