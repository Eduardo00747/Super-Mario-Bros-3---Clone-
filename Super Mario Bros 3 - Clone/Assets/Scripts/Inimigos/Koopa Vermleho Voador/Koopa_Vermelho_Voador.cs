using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa_Vermelho_Voador : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce = 10f; // Força do pulo
    public float moveSpeed = 5f; // Velocidade de movimento horizontal
    private float moveDirection = -1f; // Direção do movimento horizontal (1 para direita, -1 para esquerda)
    public float jumpInterval = 2f; // Intervalo entre cada pulo

    private float nextJumpTime;
    private SpriteRenderer spriteRenderer; // Adicionado para controle do FlipX

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Inicializa o SpriteRenderer
        nextJumpTime = Time.time + jumpInterval; // Define o próximo tempo de pulo
    }

    void Update()
    {
        // Checa se é hora de pular novamente
        if (Time.time >= nextJumpTime)
        {
            Jump();
            nextJumpTime = Time.time + jumpInterval; // Reseta o tempo para o próximo pulo
        }

        Move();
    }

    private void Jump()
    {
        // Aplica uma força de pulo
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    private void Move()
    {
        // Move o objeto horizontalmente
        rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
        // Ajusta o FlipX do SpriteRenderer baseado na direção do movimento
        spriteRenderer.flipX = moveDirection > 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barricada"))
        {
            moveDirection *= -1;
        }
    }
}
