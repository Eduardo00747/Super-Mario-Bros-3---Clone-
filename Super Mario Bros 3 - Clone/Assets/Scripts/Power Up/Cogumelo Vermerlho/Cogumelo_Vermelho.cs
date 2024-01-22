using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cogumelo_Vermelho : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar se a colisão ocorreu com a tag "Player" ou "Borda"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destruir o objeto que possui este script (o Machado)
            Destroy(gameObject);
        }
    }
}
