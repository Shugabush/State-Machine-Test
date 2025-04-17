using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    Enemy enemy;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == enemy.playerTarget)
        {
            enemy.IsAggroed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == enemy.playerTarget)
        {
            enemy.IsAggroed = false;
        }
    }
}
