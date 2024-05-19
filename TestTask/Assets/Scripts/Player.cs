using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth;
    [SerializeField] private Gradient playerHealthColor;

    private float _playerHealth;
    private Material _playerMaterial;
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = maxHealth;
        _playerMaterial = GetComponent<MeshRenderer>().material;
        UpdateColor();
    }

    public void TakeDamage()
    {
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
        if(closestEnemy != null)
        {
            Destroy(closestEnemy.gameObject);
        }
    }

    private void UpdateColor()
    {
        _playerMaterial.color = playerHealthColor.Evaluate(_playerHealth / maxHealth);
    }
}
