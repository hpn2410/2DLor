using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D currentRb;
    private void Awake()
    {
        currentRb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockBack(Transform WeaponAttack, float knockBackThurst)
    {
        gettingKnockedBack = true;
        Vector2 difference = (transform.position - WeaponAttack.position).normalized
            * knockBackThurst * currentRb.mass;
        currentRb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        currentRb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
