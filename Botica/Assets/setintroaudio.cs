using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setintroaudio : MonoBehaviour
{
    AudioPlayer audioPlayer;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        audioPlayer.changeTrack("intro");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
