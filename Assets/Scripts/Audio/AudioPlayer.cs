using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> playList = new List<AudioClip>();
    [SerializeField] private AudioSource audioSource;

    private int currentSoundIndex = 0;
    private AudioClip currentClip;

    private void Start()
    {
        currentClip = playList[currentSoundIndex];

        audioSource.clip = currentClip;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void PlayOneShot()
    {
        audioSource.PlayOneShot(currentClip);
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void PlayNextClip()
    {
        currentSoundIndex++;

        currentSoundIndex = (currentSoundIndex > playList.Count - 1) ? 0 : currentSoundIndex;

        Play();
    }
}
