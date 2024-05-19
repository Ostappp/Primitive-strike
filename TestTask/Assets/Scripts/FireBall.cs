using UnityEngine;

public class FireBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            player.TakeDamage();

        Destroy(gameObject);            
    }
}
