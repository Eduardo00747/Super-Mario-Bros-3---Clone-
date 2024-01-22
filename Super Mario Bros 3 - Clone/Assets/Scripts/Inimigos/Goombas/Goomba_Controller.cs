using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba_Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento
    private bool isMovingLeft = true; // Variável para controlar a direção do movimento

    // Update is called once per frame
    void Update()
    {
        // Movimento
        float horizontalMovement = isMovingLeft ? -moveSpeed : moveSpeed;
        Vector3 movement = new Vector3(horizontalMovement * Time.deltaTime, 0, 0);
        transform.Translate(movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Inverte a direção do movimento
            isMovingLeft = !isMovingLeft;
        }
    }
}
