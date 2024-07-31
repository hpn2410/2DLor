using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Chasing
    }
    private State _state;
    private EnemyPathFinding _enemyPathFinding;
    private Transform _playerTransform;

    private const float chaseRange = 5f; // Range within which the enemy starts chasing
    private const float stopChaseRange = 15f; // Range beyond which the enemy stops chasing

    private void Awake()
    {
        _enemyPathFinding = GetComponent<EnemyPathFinding>();
        _state = State.Roaming;
    }

    void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private void Update()
    {
        FindTarget();
        if (_state == State.Chasing)
        {
            if (_playerTransform != null)
            {
                _enemyPathFinding.MoveTo(_playerTransform.position);

                // Check if the player has moved out of the stopChaseRange
                float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
                if (distanceToPlayer > stopChaseRange)
                {
                    Debug.Log("Player is out of range, switch to roaming!");
                    _state = State.Roaming;
                    _playerTransform = null;
                    StartCoroutine(RoamingRoutine());
                }
            }
        }
    }

    private IEnumerator RoamingRoutine()
    {
        while (_state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            _enemyPathFinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        float roamRadius = 5f;
        Vector2 roamPosition = (Vector2)transform.position + 
            new Vector2(Random.Range(-roamRadius, roamRadius), Random.Range(-roamRadius, roamRadius));
        return roamPosition;
    }

    private void FindTarget()
    {
        if (PlayerManagement.instance == null)
        {
            Debug.LogError("PlayerManagement.instance is null");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, 
            PlayerManagement.instance.transform.position);
        if (distanceToPlayer < chaseRange)
        {
            Debug.Log("Player found, start chasing!");
            _state = State.Chasing;
            _playerTransform = PlayerManagement.instance.transform;
        }
    }
}
