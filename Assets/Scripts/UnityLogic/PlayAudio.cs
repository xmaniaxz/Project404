using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource),typeof(BoxCollider))]
public class PlayAudio : MonoBehaviour
{
    public bool PlayWhenLookingAt;
    public bool PlayOnAwake;
    public int TimePlayed;
    public bool isPlaying;
    private AudioSource Source;
    public void Start() => Source = GetComponent<AudioSource>();

    public void Play()
    {
        isPlaying = true;
        Source.time = TimePlayed;
        Source.Play();
    }
    public void Pause()
    {
        isPlaying = false;
        Source.Pause();
    }
    public void Update()
    {
        TimePlayed = (int)Source.time;
    }
}
