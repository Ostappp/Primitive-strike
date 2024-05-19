using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private string _targetTag;
    private Transform _target;

    [SerializeField, Min(.1f)]
    private float _entitySpeed;
    [SerializeField, Min(2)]
    private float _dontMoveDistance; // distance where entity don't come closer to the target

    [SerializeField]
    private GameObject _fireBallPrefab;    
    [SerializeField]
    private float _fireBallSpeed;

    [SerializeField, Min(0)]
    private float _attackDistance;
    [SerializeField]
    private float _attackDelay;

    private bool _canAttack = true;

    [SerializeField]
    private Transform _head;
    private Rigidbody _rigidbody;
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag(_targetTag).transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            SetLook();
            if (IsGrounded() && TargetDistance() > _dontMoveDistance)
            {
                Move();
            }
            if (_canAttack && TargetDistance() <= _attackDistance)
            {
                Attack();
            }
        }
        else
            _target = GameObject.FindGameObjectWithTag(_targetTag).transform;
    }
    private void SetLook()
    {
        _head.LookAt(_target);
        //rotate entity to look at player
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _head.eulerAngles.y, transform.eulerAngles.z);
    }
    private void Move()
    {
        Vector3 velocity = transform.forward * _entitySpeed;
        velocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = velocity;
    }
    private void Attack()
    {
        //Create fireball
        GameObject fireBall = Instantiate(_fireBallPrefab, _head.position + _head.forward * 2, Quaternion.identity);
        fireBall.transform.LookAt(_target);
        
        //Send fireball into a player
        var fRb = fireBall.GetComponent<Rigidbody>();
        fRb.useGravity = false;
        fRb.velocity = _head.forward * _fireBallSpeed;

        //Reload
        _canAttack = false;
        StartCoroutine(ResetAttack());
    }




    private float TargetDistance()
    {
        return Vector3.Distance(_head.position, _target.position);
    }
    private bool IsGrounded()
    {
        return Physics.CheckBox(transform.position, new Vector3(.55f, .1f, .55f), transform.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(.55f, .1f, .55f) * 2);
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(_attackDelay);
        _canAttack = true;
    }
}
