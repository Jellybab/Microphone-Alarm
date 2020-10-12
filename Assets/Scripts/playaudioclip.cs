using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playaudioclip : MonoBehaviour
{

    public AudioClip alarm;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playAudioClip() {
        audioSource.PlayOneShot(alarm);
    }
}
