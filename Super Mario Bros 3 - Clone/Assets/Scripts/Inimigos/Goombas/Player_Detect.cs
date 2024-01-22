using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Detect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar se o objeto colidido tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Acessar o objeto pai (Goomba) e alterar a tag para "Enemy"
            Transform parentObject = transform.parent;
            if (parentObject != null)
            {
                parentObject.gameObject.tag = "Enemy";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verificar se o objeto que não está mais colidindo tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Acessar o objeto pai (Goomba) e alterar a tag para "EnemyBounce"
            Transform parentObject = transform.parent;
            if (parentObject != null)
            {
                parentObject.gameObject.tag = "EnemyBounce";
            }
        }
    }
}
