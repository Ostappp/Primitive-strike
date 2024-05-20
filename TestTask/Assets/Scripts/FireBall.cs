using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireBall : MonoBehaviour
{
    [SerializeField] private AudioClip burstSound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Play burst sound
        _audioSource.PlayOneShot(burstSound);

        //Make fireball invisible & stop moving
        Destroy(GetComponent<MeshRenderer>());
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());


        if (collision.gameObject.TryGetComponent(out Player player))
            player.TakeDamage();

        Destroy(gameObject, 1);       
    }
}
