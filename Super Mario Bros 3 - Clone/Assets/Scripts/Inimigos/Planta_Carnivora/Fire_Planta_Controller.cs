using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Planta_Controller : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidade de rotação, em graus por segundo

    // Update is called once per frame
    void Update()
    {
        // Rotaciona o objeto no sentido anti-horário em torno do eixo Z
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")||other.CompareTag("Ground")||other.CompareTag("Box"))
        {
            // Destruir o objeto que possui este script (o Cogumelo)
            Destroy(gameObject);
        }
    }
}
