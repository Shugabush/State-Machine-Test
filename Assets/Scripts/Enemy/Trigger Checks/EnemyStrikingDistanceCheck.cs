using UnityEngine;

public class EnemyStrikingDistanceCheck : MonoBehaviour
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
            enemy.IsWithinStrikingDistance = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == enemy.playerTarget)
        {
            enemy.IsWithinStrikingDistance = false;
        }
    }
}
