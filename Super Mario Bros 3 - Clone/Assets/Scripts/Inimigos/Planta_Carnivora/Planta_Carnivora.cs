using UnityEngine;

public class Planta_Carnivora : MonoBehaviour
{
    public GameObject personagem; // Referência ao objeto "Personagem"
    private Animator animator;    // Referência ao Animator
    private SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer
    private Transform fireBallPlantaTransform; // Referência para o Transform do objeto filho "FireBall_Planta"

    void Start()
    {
        // Obtenha o componente Animator
        animator = GetComponent<Animator>();

        // Obtenha o componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Tente encontrar o objeto filho "FireBall_Planta"
        fireBallPlantaTransform = transform.Find("FireBall_Planta");
    }

    void Update()
    {
        // Verifica se há uma referência para o personagem
        if (personagem != null)
        {
            // Itera sobre todos os filhos do objeto "Personagem"
            foreach (Transform child in personagem.transform)
            {
                if (child.position.y > transform.position.y)
                {
                    // Se a posição em Y de qualquer filho for maior que a posição em Y da planta, define o parâmetro "isTarget_Up" como true
                    animator.SetBool("isTarget_Up", true);
                    spriteRenderer.flipX = child.position.x > transform.position.x;

                    // Ajusta a posição do objeto filho "FireBall_Planta" com base no estado de FlipX
                    if (fireBallPlantaTransform != null)
                    {
                        fireBallPlantaTransform.localPosition = new Vector3(spriteRenderer.flipX ? 0.09f : -0.1f, fireBallPlantaTransform.localPosition.y, fireBallPlantaTransform.localPosition.z);
                    }

                    return; // Encerra o loop e a função Update
                }
            }

            // Se nenhum filho estiver acima da planta, define o parâmetro "isTarget_Up" como false
            animator.SetBool("isTarget_Up", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desativar o collider do objeto 
            GetComponent<BoxCollider2D>().enabled = false; // Desativa o BoxCollider2D
        }
    }
}