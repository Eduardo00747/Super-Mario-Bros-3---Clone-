using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Controller : MonoBehaviour
{  
    private Animator animator;
    private Rigidbody2D rb;

    //Outras Variaveis 
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
   
        animator.SetBool("isDeath", false); // Ativa a animação
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar se a colisão ocorreu 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(DeathSequence());
        }
    }

    private IEnumerator DeathSequence()
    {

        // Ativa o isTrigger do BoxCollider2D
        GetComponent<BoxCollider2D>().isTrigger = true;
        
        animator.SetBool("isDeath", true); // Ativa a animação
        yield return new WaitForSeconds(0.1f);

        // Adiciona um pequeno impulso vertical
        rb.velocity = new Vector2(rb.velocity.x, 5f);

        // Congela a posição X (impede o movimento horizontal)
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        // Aguarda um curto período de tempo antes de destruir o objeto
        yield return new WaitForSeconds(1.4f);

        // Destruir o objeto que possui este script 
        Destroy(gameObject);
    }
}
