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

        UpdateAudio();
        SoundManager.Instance.ChangedSettings += UpdateAudio;

        
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

    private void OnDestroy()
    {
        SoundManager.Instance.ChangedSettings -= UpdateAudio;
    }


    private void UpdateAudio()
    {
        _audioSource.volume = SoundManager.Instance ? SoundManager.Instance.GetVolume() : 1f;
    }
}
