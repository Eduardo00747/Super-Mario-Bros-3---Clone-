using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Planta_Controller : MonoBehaviour
{
    public GameObject fireballPrefab; // Referência ao prefab da bola de fogo
    public GameObject personagem; // Referência ao objeto "Personagem"
    public float fireballSpeed = 5f; // Velocidade da bola de fogo

    // Start is called before the first frame update
    void Start()
    {
        // Inicia a corrotina para instanciar duas bolas de fogo com um intervalo
        StartCoroutine(InstantiateTwoFireballsWithInterval());
    }

    private IEnumerator InstantiateTwoFireballsWithInterval()
    {
        // Primeira bola de fogo após 3 segundos
        yield return StartCoroutine(InstantiateFireballAfterDelay(7f));

        // Aguarda mais 4 segundos
        yield return new WaitForSeconds(5f);

        // Segunda bola de fogo
        yield return StartCoroutine(InstantiateFireballAfterDelay(0f)); // Sem espera adicional
    }

    private IEnumerator InstantiateFireballAfterDelay(float delay)
    {
        // Aguarda o tempo especificado
        yield return new WaitForSeconds(delay);

        // Instancia o prefab da bola de fogo
        if (fireballPrefab != null && personagem != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

            // Calcula a direção para o último filho do objeto "Personagem"
            Vector2 targetDirection = Vector2.zero;
            if (personagem.transform.childCount > 0)
            {
                Transform lastChild = personagem.transform.GetChild(personagem.transform.childCount - 1);
                targetDirection = (lastChild.position - transform.position).normalized;
            }

            // Aplica a força na direção do último filho do objeto "Personagem"
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = targetDirection * fireballSpeed;
            }
        }
        else
        {
            Debug.LogError("Fireball prefab ou Personagem não está definido!");
        }
    }
}
