using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Grande_Mario_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7.5f;
    private Rigidbody2D rb;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool canJump = false;

    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        // Verifica se a CinemachineVirtualCamera foi encontrada
        if (virtualCamera != null)
        {
            // Configura o Follow para seguir este objeto
            virtualCamera.Follow = transform;
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera não encontrada na cena!");
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.8f, groundLayer);

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0);

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        bool isWalk_Grande = Mathf.Abs(horizontalInput) > 0.1f;
        animator.SetBool("isWalk_Grande", isWalk_Grande);

        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Box"))
        {
            canJump = true;
            animator.SetBool("isJump_Grande", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Box"))
        {
            canJump = false;
            animator.SetBool("isJump_Grande", true);
        }
    }
}