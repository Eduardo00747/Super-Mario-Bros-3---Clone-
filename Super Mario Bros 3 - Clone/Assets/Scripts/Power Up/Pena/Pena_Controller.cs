using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pena_Controller : MonoBehaviour
{
    public float verticalSpeed = -2f; // Velocidade vertical (negativa para descer)
    public float horizontalAmplitude = 1f; // Amplitude do movimento horizontal
    public float horizontalFrequency = 1f; // Frequência do movimento horizontal

    private float startTime;

    private Rigidbody2D rb;
    private bool shouldMove = true; // Controla se o objeto deve continuar se movimentando

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time; // Registra o tempo inicial
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            // Movimento enquanto shouldMove for true
            float horizontalOffset = horizontalAmplitude * Mathf.Sin((Time.time - startTime) * horizontalFrequency);
            transform.position += new Vector3(horizontalOffset, verticalSpeed * Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se colidiu com "Player", "Ground" ou "Box"
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            shouldMove = false; // Para o movimento
        }

        // Verifica se colidiu com "Player", "Ground" ou "Box"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destrói o objeto atual
            Destroy(gameObject);
        }
    }
}
