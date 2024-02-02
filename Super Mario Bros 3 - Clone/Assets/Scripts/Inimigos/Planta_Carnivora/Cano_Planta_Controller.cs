using UnityEngine;
using System.Collections;

public class Cano_Planta_Controller : MonoBehaviour
{
    public GameObject personagem; // Referência ao objeto "Personagem"
    public GameObject plantaCarnivoraPrefab; // Referência ao prefab "Planta Carnívora"
    public float distanciaDeDetecção = 5f; // Distância para detecção do personagem
    public float forcaSubida = 5f; // Força aplicada para a subida da planta
    private bool plantaSubindo = false; // Controla se a planta já está subindo
    private Rigidbody2D rbPlanta; // Referência ao Rigidbody da planta
    private Vector3 posicaoInicial; // Guarda a posição inicial da planta


    // Start is called before the first frame update
    void Start()
    {
        // Certifique-se de que o prefab "Planta Carnívora" tem um componente Rigidbody2D
        rbPlanta = plantaCarnivoraPrefab.GetComponent<Rigidbody2D>();

        posicaoInicial = plantaCarnivoraPrefab.transform.position; // Armazena a posição inicial da planta

    }

    // Update is called once per frame
    void Update()
    {
        if (personagem != null && !plantaSubindo)
        {
            foreach (Transform filho in personagem.transform)
            {
                // Calcula a distância entre o cano e o filho do personagem
                float distancia = Vector3.Distance(transform.position, filho.position);

                // Verifica se a distância é menor que a distância de detecção
                if (distancia < distanciaDeDetecção)
                {
                    Debug.Log("Personagem próximo");
                    plantaSubindo = true; // Marca que a planta está subindo

                    // Inicia a corrotina para aplicar a força de subida
                    StartCoroutine(AplicarForcaSubida());
                    break; // Encerra o loop se um filho já estiver próximo
                }
            }
        }
    }

private IEnumerator AplicarForcaSubida()
{
    // Aplica a força de subida
    rbPlanta.AddForce(Vector2.up * forcaSubida, ForceMode2D.Force);

    // Espera por 3 segundos
    yield return new WaitForSeconds(1.37f);

    // Ativa o script do objeto chamado "FireBall_Planta_Controller"
    // Aqui, você procura pelo script "FireBall_Planta_Controller" no prefab da planta carnívora e o ativa.
    FireBall_Planta_Controller fireBallPlantaController = plantaCarnivoraPrefab.GetComponentInChildren<FireBall_Planta_Controller>();
    if (fireBallPlantaController != null)
    {
        fireBallPlantaController.enabled = true; // Ativa o script "FireBall_Planta_Controller"
    }
    else
    {
        Debug.LogError("Não foi possível encontrar o script FireBall_Planta_Controller no prefab plantaCarnivora.");
    }

    // Para de aplicar a força após 1.3 segundos
    rbPlanta.velocity = Vector2.zero;

    // Aguarda 10 segundos antes de iniciar a descida
    yield return new WaitForSeconds(10f);

    // Inicia a descida suave até a posição inicial
    StartCoroutine(VoltarPosicaoInicialSuave());
}

private IEnumerator VoltarPosicaoInicialSuave()
{
    float duracaoDescida = 55f; // Tempo que levará para a planta voltar à posição inicial
    float tempoInicio = Time.time;

    while (Time.time < tempoInicio + duracaoDescida)
    {
        // Calcula o progresso da interpolação baseado no tempo passado
        float progresso = (Time.time - tempoInicio) / duracaoDescida;

        // Interpola a posição atual da planta para a posição inicial suavemente
        plantaCarnivoraPrefab.transform.position = Vector3.Lerp(plantaCarnivoraPrefab.transform.position, posicaoInicial, progresso);

        yield return null; // Aguarda o próximo frame antes de continuar o loop
    }

    // Garante que a planta esteja exatamente na posição inicial ao final da interpolação
    plantaCarnivoraPrefab.transform.position = posicaoInicial;
    plantaSubindo = false;
}
}
