using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    [SerializeField]
    private EnemyBehaviour enemyBehaviour;

    [SerializeField]
    private Transform rotationTransform;

    // Update is called once per frame
    void Update()
    {
        if (!enemyBehaviour.PlayerCommonData) return;

        Vector2 dir = enemyBehaviour.PlayerCommonData.PlayerPosition - (Vector2)rotationTransform.position;
        dir.Normalize();

        float angle = Mathf.Atan2(dir.y, dir.x);
        rotationTransform.rotation = new Quaternion(0, 0, Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));
    }
}
