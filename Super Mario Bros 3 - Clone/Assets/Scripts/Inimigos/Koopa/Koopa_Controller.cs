using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa_Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento
    private bool isMovingLeft = true; // Variável para controlar a direção do movimento

    private SpriteRenderer spriteRenderer; // Referência para o SpriteRenderer do objeto

    void Start()
    {
        // Obtém a referência ao SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimento
        float horizontalMovement = isMovingLeft ? -moveSpeed : moveSpeed;
        Vector3 movement = new Vector3(horizontalMovement * Time.deltaTime, 0, 0);
        transform.Translate(movement);

        // Atualiza o estado do flipX com base na direção do movimento
        spriteRenderer.flipX = !isMovingLeft;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Inverte a direção do movimento
            isMovingLeft = !isMovingLeft;
        }
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barricada"))
        {
            // Inverte a direção do movimento
            isMovingLeft = !isMovingLeft;
        }
    }
}
