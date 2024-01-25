using System.Collections;
using UnityEngine;

public class Create_Multiple_Coin : MonoBehaviour
{
    public GameObject prefabToInstantiate; // A referência ao seu prefab
    private int vezesExecutadas = 0; // Contador de execuções

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifique se a colisão ocorreu com o jogador na parte inferior
        ContactPoint2D contact = collision.contacts[0];
        if (collision.gameObject.CompareTag("Player") && contact.normal.y > 0.5f && vezesExecutadas < 5)
        {
            StartCoroutine(InstantiateItemWithDelay());
        }
    }

    private IEnumerator InstantiateItemWithDelay()
    {
        // Aguarde um curto período de tempo
        yield return new WaitForSeconds(0.1f);

        // Instancie o prefab na posição atual, adicionando um impulso no eixo Y
        GameObject newItem = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        Rigidbody2D itemRb = newItem.GetComponent<Rigidbody2D>();
        if (itemRb != null)
        {
            itemRb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }

        // Incrementa o contador de execuções
        vezesExecutadas++;
    }
}
