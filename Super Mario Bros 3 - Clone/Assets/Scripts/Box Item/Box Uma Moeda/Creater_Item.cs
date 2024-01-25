using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater_Item : MonoBehaviour
{
    public GameObject prefabToInstantiate; // A referência ao seu prefab
    private bool hasBeenTriggered = false; // Flag para verificar se a lógica já foi acionada

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifique se a colisão ocorreu com o jogador na parte inferior
        ContactPoint2D contact = collision.contacts[0];
        if (collision.gameObject.CompareTag("Player") && contact.normal.y > 0.5f && !hasBeenTriggered)
        {
            StartCoroutine(InstantiateItemWithDelay());
        }
    }

    private IEnumerator InstantiateItemWithDelay()
    {
        // Defina a flag para garantir que essa lógica só seja acionada uma vez
        hasBeenTriggered = true;

        // Aguarde 3 segundos
        yield return new WaitForSeconds(0.1f);

        // Instancie o prefab na posição atual, adicionando um impulso no eixo Y
        GameObject newItem = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        Rigidbody2D itemRb = newItem.GetComponent<Rigidbody2D>();
        if (itemRb != null)
        {
            itemRb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }
}
