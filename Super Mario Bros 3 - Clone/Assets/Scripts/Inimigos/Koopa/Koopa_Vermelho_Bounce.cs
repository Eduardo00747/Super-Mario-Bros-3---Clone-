using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa_Vermelho_Bounce : MonoBehaviour
{

    public GameObject prefabToInstantiate; // Referência ao prefab a ser instanciado

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar se a colisão ocorreu com a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Acessar o objeto pai (Goomba)
            Transform parentObject = transform.parent;
            if (parentObject != null)
            {
                // Iniciar a coroutine para destruir o objeto pai após 0.3 segundos
                StartCoroutine(DestroyParentObject(parentObject.gameObject));
            }
        }
    }

    // Coroutine para destruir o objeto pai após um determinado tempo
    private IEnumerator DestroyParentObject(GameObject parentObject)
    {
        yield return new WaitForSeconds(0f);

        // Instancia o prefab antes de destruir o objeto pai
        if (prefabToInstantiate != null)
        {
            Instantiate(prefabToInstantiate, parentObject.transform.position, Quaternion.identity);
        }

        // Destruir o objeto pai (Goomba)
        Destroy(parentObject);
    }
}
