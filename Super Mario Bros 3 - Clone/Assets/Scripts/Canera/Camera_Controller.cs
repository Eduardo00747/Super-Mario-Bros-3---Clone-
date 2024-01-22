using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Controller : MonoBehaviour
{
    public Transform mario_Pequeno_Transform; // Referência ao transform do objeto chamado "****************Personagem *******************"
    public CinemachineVirtualCamera virtualCamera; // Referência à sua CinemachineVirtualCamera

    // Start is called before the first frame update
    void Start()
    {
        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera não atribuída no Inspector!");
            return;
        }

        // Certifique-se de que o Follow está ativo na virtualCamera
        if (virtualCamera.Follow == null)
        {
            virtualCamera.Follow = mario_Pequeno_Transform;
        }
    }

}
