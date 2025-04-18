using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource; 
    public NPCSensor npcSensor;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.Play();

            npcSensor.HearAudio();
        }
    }
}
