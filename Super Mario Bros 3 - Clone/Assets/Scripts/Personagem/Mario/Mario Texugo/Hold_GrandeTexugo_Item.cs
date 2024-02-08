using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold_GrandeTexugo_Item : MonoBehaviour
{
    private GameObject objectToHold;
    private bool canHold = false;
    private bool isHolding = false;
    private SpriteRenderer spriteRenderer;

    public Transform holdItemTransform; // Referência para o Transform do objeto Hold_Item

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        // Verifica se a tecla 'K' é pressionada e há um objeto para segurar
        if (Input.GetKeyDown(KeyCode.K) && canHold && objectToHold != null)
        {
            HoldObject();
            animator.SetBool("isHold_Texugo", true);

        }

        // Verifica se a tecla 'K' foi solta e o personagem está segurando um objeto
        if (Input.GetKeyUp(KeyCode.K) && isHolding)
        {
            ReleaseObject();
        }

        else if (!isHolding) // Quando não estiver segurando o objeto
        {
            // Desativa as animações relacionadas
            animator.SetBool("isHold_Texugo", false);
        }

        // Ajusta a posição do Hold_Item com base na orientação do personagem
        if (spriteRenderer.flipX)
        {
            holdItemTransform.localPosition = new Vector3(-0.3f, holdItemTransform.localPosition.y, holdItemTransform.localPosition.z);
        }
        else
        {
            holdItemTransform.localPosition = new Vector3(0f, holdItemTransform.localPosition.y, holdItemTransform.localPosition.z);
        }
        if (isHolding)
        {
            // Ajusta a posição do objeto segurado com base na orientação do personagem
            float offsetX = spriteRenderer.flipX ? -0.16f : 0.16f;
            objectToHold.transform.localPosition = new Vector3(offsetX, objectToHold.transform.localPosition.y, objectToHold.transform.localPosition.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Casco"))
        {
            // Guarda uma referência ao objeto para segurar
            objectToHold = collision.gameObject;
            canHold = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Casco"))
        {
            // Limpa a referência ao objeto quando ele sai do trigger
            if (!isHolding)
            {
                objectToHold = null;
                canHold = false;
            }
        }
    }
    private void HoldObject()
    {
        // Define o objeto como filho do personagem
        objectToHold.transform.SetParent(transform);

        // Ajusta a posição local do objeto segurado
        float offsetX = spriteRenderer.flipX ? -0.2f : 0.2f;
        objectToHold.transform.localPosition = new Vector3(offsetX, objectToHold.transform.localPosition.y, objectToHold.transform.localPosition.z);

        // Desativa a física
        Rigidbody2D rb = objectToHold.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false; // Desativa a simulação física
        }

        isHolding = true;
    }

    private void ReleaseObject()
    {
        // Solta o objeto
        if (objectToHold != null)
        {
            objectToHold.transform.SetParent(null);

            // Reativa a física
            Rigidbody2D rb = objectToHold.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.simulated = true; // Reativa a simulação física

                // Inicia a Coroutine para mudar a tag e ativar o script
                StartCoroutine(ChangeTagAndActivateScript(rb));

                // Determina a direção da força com base na orientação do personagem
                float forceX = spriteRenderer.flipX ? -5f : 5f; // Inverte a direção da força
                Vector2 releaseForce = new Vector2(forceX, 2f); // Ajuste os valores conforme necessário
                rb.AddForce(releaseForce, ForceMode2D.Impulse);
            }
            objectToHold = null;
        }
        isHolding = false;
        canHold = false;

        // Ativa a animação de chute
        animator.SetBool("isKick_Texugo", true);

        // Inicia a Coroutine para desativar a animação após o término
        StartCoroutine(DisableKickAnimation());
    }

    private IEnumerator ChangeTagAndActivateScript(Rigidbody2D rb)
    {
        // Aguarda 0.1 segundo
        yield return new WaitForSeconds(0.1f);

        // Muda a tag do objeto e ativa o script Casco_Controller
        if (rb.gameObject != null)
        {
            rb.gameObject.tag = "Casco_Vermelho";
            Casco_Controller cascoController = rb.gameObject.GetComponent<Casco_Controller>();
            if (cascoController != null)
            {
                cascoController.enabled = true;
            }
        }
    }

    private IEnumerator DisableKickAnimation()
    {
        // Aguarda um pequeno período de tempo (ajuste conforme a duração da animação)
        yield return new WaitForSeconds(0.1f); // Substitua 0.5 pelo tempo de duração da animação de chute

        // Desativa a animação de chute
        animator.SetBool("isKick_Texugo", false);
    }
}
