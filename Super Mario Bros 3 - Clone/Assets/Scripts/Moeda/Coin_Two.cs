using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Two : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destruir o objeto que possui este script (o Cogumelo)
            Destroy(gameObject);
        }
    }
}
