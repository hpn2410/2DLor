using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private Animator weaponAnim;
    private PlayerManagement playerManagement;
    private PlayerStats playerStats;
    public WeaponSO weaponSOs;
    // Start is called before the first frame update
    void Start()
    {
        weaponAnim = GetComponent<Animator>();
        playerManagement = gameObject.GetComponentInParent<PlayerManagement>();
        playerStats = gameObject.GetComponentInParent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseFollow();
        CheckIfAttack();
    }

    private void MouseFollow()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint
            (playerManagement.transform.position);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            for (int i = 0; i < playerStats.weaponList.Length; i++)
            {
                playerStats.weaponList[i].transform.rotation = Quaternion.Euler(0, 180, -angle);
            }
        }
        else
        {
            for (int i = 0; i < playerStats.weaponList.Length; i++)
            {
                playerStats.weaponList[i].transform.rotation = Quaternion.Euler(0, 0, -angle);
            }
        }
    }

    private void CheckIfAttack()
    {
        if (playerStats.hasWeapon && Input.GetMouseButtonDown(0))
        {
            weaponAnim.SetBool("isAttack", true);
        }
        else
            weaponAnim.SetBool("isAttack", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        BossAI bossAI = collision.gameObject.GetComponent<BossAI>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyHealth.TakeDamage(weaponSOs.damage);
        }
        else if (collision.gameObject.CompareTag("Boss") && !bossAI.isShielding)
        {
            bossAI.TakeDamage(weaponSOs.damage);
        };
    }
}
