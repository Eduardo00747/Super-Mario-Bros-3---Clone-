using UnityEngine;

public class Casco_Controller : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barricada"))
        {
            // Inverte a direção quando colide com a tag "Barricada"
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
    }
}
