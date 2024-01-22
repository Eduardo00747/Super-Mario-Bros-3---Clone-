using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    public GameObject marioPrefab; // Referência ao Prefab do Mario

    // Start is called before the first frame update
    void Start()
    {
        // Verifica se o Prefab do Mario foi atribuído
        if (marioPrefab != null)
        {
            // Instancia o objeto do Mario na posição inicial desse script (Controller_Player)
            GameObject marioInstance = Instantiate(marioPrefab, transform.position, Quaternion.identity);

            // Define o objeto chamado "****************Personagem *******************" como pai do objeto instanciado
            Transform personagemTransform = FindPersonagemTransform();
            if (personagemTransform != null)
            {
                marioInstance.transform.parent = personagemTransform;
            }
            else
            {
                Debug.LogError("Objeto chamado '****************Personagem *******************' não encontrado!");
            }
        }
        else
        {
            Debug.LogError("Prefab do Mario não atribuído no Inspector!");
        }
    }

    // Encontrar o transform do objeto chamado "****************Personagem *******************"
    private Transform FindPersonagemTransform()
    {
        Transform[] transforms = GameObject.FindObjectsOfType<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.name == "****************Personagem *******************")
            {
                return t;
            }
        }
        return null;
    }
}
