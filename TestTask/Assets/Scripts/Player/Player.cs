using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth;
    [SerializeField] private Gradient playerHealthColor;
    public Color AttackColor;

    private float _playerHealth;
    private Material _playerMaterial;

    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip hurtSound;
    private AudioSource _playerAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = maxHealth;
        _playerMaterial = GetComponent<MeshRenderer>().material;
        _playerAudioSource = GetComponent<AudioSource>();
        _playerAudioSource.playOnAwake = false;


        UpdateAudio();
        UpdateColor();

        SoundManager.Instance.ChangedSettings += UpdateAudio;
    }
    private void OnDestroy()
    {
        SoundManager.Instance.ChangedSettings -= UpdateAudio;
    }
    public void TakeDamage()
    {
        PlayAudio();
        _playerHealth--;

        if (_playerHealth < 0)
            _playerHealth = maxHealth;

        UpdateColor();
    }

    public void Attack()
    {
        List<Collider> hits = Physics.OverlapSphere(transform.position, 2000f).ToList();// get all entities in a large sphere
        var closestEnemy = hits
            .Select(hit => hit.GetComponentInParent<EnemyAI>()).Where(enemy => enemy != null)// get all enemies
            .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))// sort by distance from player
            .FirstOrDefault();//get closest enemy
        if (closestEnemy != null)
        {
            AttackEffect(closestEnemy.transform);
            Destroy(closestEnemy.gameObject);
        }
    }
    private void AttackEffect(Transform enemy)
    {
        //Play sound
        PlayAudio(false);

        GameObject killingRay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(killingRay.GetComponent<Collider>());

        // Set position, rotation and scale
        killingRay.transform.position = (transform.position + enemy.position) / 2;
        killingRay.transform.LookAt(enemy);
        killingRay.transform.Rotate(Vector3.right, 90f);
        killingRay.transform.localScale = 0.5f * Vector3.up * Vector3.Distance(transform.position, enemy.position) + (Vector3.forward + Vector3.right) * .1f;

        // Paint ray        
        killingRay.GetComponent<MeshRenderer>().material.color = AttackColor;

        Destroy(killingRay, 0.5f);
    }
    private void PlayAudio(bool damageTaken = true)
    {
        if (damageTaken)
            _playerAudioSource.PlayOneShot(hurtSound);
        else
            _playerAudioSource.PlayOneShot(shootSound);
    }
    private void UpdateColor()
    {
        _playerMaterial.color = playerHealthColor.Evaluate(_playerHealth / maxHealth);
    }
    private void UpdateAudio()
    {
        _playerAudioSource.volume = SoundManager.Instance ? SoundManager.Instance.GetVolume() : 1f;
    }
}
