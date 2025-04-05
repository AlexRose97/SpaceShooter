using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("BulletEnemy") || other.CompareTag("BulletPlayer"))
        {
            Destroy(other.gameObject);
        }
    }
}
