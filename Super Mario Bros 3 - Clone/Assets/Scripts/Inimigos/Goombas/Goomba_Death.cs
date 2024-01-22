using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba_Death : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar se a colisão ocorreu com a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Acessar o objeto pai (Goomba)
            Transform parentObject = transform.parent;
            if (parentObject != null)
            {
                // Acessar o componente Animator do objeto pai
                Animator goombaAnimator = parentObject.GetComponent<Animator>();

                // Se o Animator for válido, definir o parâmetro isGoomba_Death como true
                if (goombaAnimator != null)
                {
                    goombaAnimator.SetBool("isGoomba_Death", true);

                    // Iniciar a coroutine para destruir o objeto pai após 0.3 segundos
                    StartCoroutine(DestroyParentObject(parentObject.gameObject));

                }
            }
        }
    }

    // Coroutine para destruir o objeto pai após um determinado tempo
    private IEnumerator DestroyParentObject(GameObject parentObject)
    {
        yield return new WaitForSeconds(0.3f);

        // Destruir o objeto pai (Goomba)
        Destroy(parentObject);
    }
}
