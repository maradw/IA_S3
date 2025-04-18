using UnityEngine;
using Cinemachine;

public class TopDownCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform npc; // El NPC al que la c�mara debe seguir

    void Start()
    {
        // Aseg�rate de que la c�mara sigue al NPC
        if (virtualCamera != null && npc != null)
        {
            virtualCamera.Follow = npc;
            virtualCamera.LookAt = npc;

            // Ajuste de la vista top-down
            var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null)
            {
                // Posiciona la c�mara sobre el NPC (10 unidades arriba)
                transposer.m_FollowOffset = new Vector3(0, 10, 0);
            }
        }
    }
}
