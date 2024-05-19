using UnityEngine;

public class FireBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerControls player))
            player.Damage();

        Destroy(gameObject);            
    }
}
