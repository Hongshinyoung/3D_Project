using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeTime;
    public float maxVolume;
    private float targetVolume;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        targetVolume = 0;
        audioSource.volume = targetVolume;
        audioSource.Play();
    }


    private void Update()
    {
        if (!Mathf.Approximately(audioSource.volume, targetVolume)) //Approximately: 근사값이면 같도록 처리해준다.
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, (maxVolume / fadeTime) * Time.deltaTime); //MoveTowards(a,b,c): 점점 a에서 b까지 커지는데 c의 속도로 커진다.

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = maxVolume;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = 0;
        }
    }
}
