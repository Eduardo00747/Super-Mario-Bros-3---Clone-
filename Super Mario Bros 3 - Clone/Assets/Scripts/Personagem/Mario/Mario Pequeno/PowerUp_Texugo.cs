using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Texugo : MonoBehaviour
{
    public GameObject marioGrandePrefab; // Referência ao Prefab do Mario

    private Animator animator;

    //Outras Variaveis 
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Desativa "Mario Grande" no início
        animator.SetBool("isTexugo_Trans", false); // Ativa a animação
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a colisão ocorreu com a tag "Cogumelo"
        if (collision.gameObject.CompareTag("Folha_Texugo"))
        {
            StartCoroutine(PerformPowerUpSequence());
        }
    }

    private IEnumerator PerformPowerUpSequence()
    {
        // Ativa a animação
        animator.SetBool("isTexugo_Trans", true);

        // Aguarda o término da animação (tempo pode variar, ajuste conforme necessário)
        yield return new WaitForSeconds(0.3f);

        // Instancia o objeto "marioGrandePrefab" na posição atual
        GameObject marioGrandeInstance = Instantiate(marioGrandePrefab, transform.position, Quaternion.identity);

        // Encontrar o transform do objeto chamado "****************Personagem *******************"
        Transform personagemTransform = FindPersonagemTransform();
        if (personagemTransform != null)
        {
            // Define o objeto instanciado como filho do objeto "****************Personagem *******************"
            marioGrandeInstance.transform.parent = personagemTransform;
        }
        else
        {
            Debug.LogError("Objeto chamado '****************Personagem *******************' não encontrado!");
        }

        // Destrói o objeto atual
        Destroy(gameObject);
    }

    // Encontrar o transform do objeto chamado "****************Personagem *******************"
    private Transform FindPersonagemTransform()
    {
        GameObject personagemObject = GameObject.Find("****************Personagem *******************");
        if (personagemObject != null)
        {
            return personagemObject.transform;
        }
        else
        {
            return null;
        }
    }
}
