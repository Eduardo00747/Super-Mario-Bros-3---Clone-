using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pena_Box_Controller : MonoBehaviour
{
    public GameObject prefabToInstantiate; // A referência ao seu prefab
    private bool hasBeenTriggered = false; // Flag para verificar se a lógica já foi acionada

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifique se a colisão ocorreu lateralmente com o jogador
        ContactPoint2D contact = collision.contacts[0];
        if (collision.gameObject.CompareTag("Casco_Vermelho") && Mathf.Abs(contact.normal.x) > Mathf.Abs(contact.normal.y) && !hasBeenTriggered)
        {
            StartCoroutine(InstantiateItemWithDelay());
        }
    }

    private IEnumerator InstantiateItemWithDelay()
    {
        // Defina a flag para garantir que essa lógica só seja acionada uma vez
        hasBeenTriggered = true;

        // Aguarde 0.1 segundo
        yield return new WaitForSeconds(0.1f);

        // Instancie o prefab na posição atual, adicionando um impulso no eixo Y
        GameObject newItem = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        Rigidbody2D itemRb = newItem.GetComponent<Rigidbody2D>();
        if (itemRb != null)
        {
            itemRb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);

            // Aguarde 0.1 segundo antes de ativar o script "Pena_Controller"
            yield return new WaitForSeconds(0.5f);

            // Ativat o script do objeto chamado "Pena_Controller"
            Pena_Controller penaController = newItem.GetComponent<Pena_Controller>();
            if (penaController != null)
            {
                penaController.enabled = true; // Ativa o script "Pena_Controller"
                // Deixar o "isTrigger" do Collider2D como false
                Collider2D collider = newItem.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.isTrigger = false;
                }
            }
        }
    }
}
