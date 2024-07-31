using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    // Get Rigidbody component
    private Rigidbody2D playerRb;

    // Define vector input from user
    private Vector3 moveInput;
    private Vector2 moveDirection;

    // Define speed
    private float walkSpeed = 4f;
    private float runSpeed = 10f;

    // Get Animator component
    private Animator playerAnimator;
    private PlayerStats playerStats;
    private KnockBack knockBack;

    public static PlayerManagement instance;

    void Start()
    {
        instance = this;
        playerRb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        playerAnimator = GetComponent<Animator>();
        knockBack = GetComponent<KnockBack>();
    }

    void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        // Get Player Input
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveDirection = new Vector2(moveInput.x, moveInput.y).normalized;
    }

    private void MovePlayer()
    {
        if (knockBack.gettingKnockedBack) { return; }
        // Check and set last moveX input and moveY input
        if (moveDirection != Vector2.zero)
        {
            playerAnimator.SetFloat("moveX", moveDirection.x);
            playerAnimator.SetFloat("moveY", moveDirection.y);
            playerAnimator.SetBool("IsMoving", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("IsRunning", true);
                transform.position += moveInput * runSpeed * Time.deltaTime;
            }
            else
            {
                playerAnimator.SetBool("IsRunning", false);
                transform.position += moveInput * walkSpeed * Time.deltaTime;
            }
        }
        else
        {
            playerAnimator.SetBool("IsMoving", false);
            playerAnimator.SetBool("IsRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DetectionZone"))
        {
            MapTransition mapTransition = collision.gameObject.GetComponent<MapTransition>();
            if (mapTransition != null)
            {
                string nextScene = mapTransition.nextScene;
                GameManager.instance.ChangeScene(nextScene);
            }
            else
            {
                Debug.LogError("MapTransition component is missing on the object with tag 'DetectionZone'");
            }
        }
    }
}
