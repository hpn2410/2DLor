using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 2f;

    private Rigidbody2D _enemyRigidbody;
    private Vector2 _moveDir;

    private KnockBack knockBack;
    public static EnemyPathFinding instance;

    private Animator enemyAnim;

    private void Start()
    {
        instance = this;
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        if (_enemyRigidbody == null)
        {
            Debug.LogError("Rigidbody2D is null on " + gameObject.name);
        }
        knockBack = GetComponent<KnockBack>();
        enemyAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (knockBack.gettingKnockedBack) { return; }
        if (_enemyRigidbody != null)
        {
            _enemyRigidbody.MovePosition(_enemyRigidbody.position + _moveDir * 
                (_moveSpeed * Time.fixedDeltaTime));
            UpdateAnimation();
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        if (_enemyRigidbody != null)
        {
            _moveDir = (targetPosition - _enemyRigidbody.position).normalized;
        }
    }

    private void UpdateAnimation()
    {
        if (_moveDir.x != 0)
        {
            enemyAnim.SetFloat("x", _moveDir.x);
            enemyAnim.SetFloat("y", _moveDir.y);
        }
    }
}
