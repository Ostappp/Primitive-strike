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
        Debug.Log($"Damage taken");
        _playerHealth--;

        if (_playerHealth < 0) 
            _playerHealth = maxHealth;

        Debug.Log($"health: {_playerHealth}/{maxHealth}");
        UpdateColor();
    }
    private void UpdateColor()
    {
        _playerMaterial.color = playerHealthColor.Evaluate(_playerHealth / maxHealth);        
    }
}
