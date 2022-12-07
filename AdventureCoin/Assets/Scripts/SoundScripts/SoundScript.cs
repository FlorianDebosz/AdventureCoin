using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sound,secondSound;

    [SerializeField] private int playedSong = 1;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playSong(playedSong);
    }

    IEnumerator WaitEndSong(int playedSong) {
        if(playedSong == 1){
            yield return new WaitForSeconds(sound.length);
            playSong(playedSong);
        }else if(playedSong == 2){
            yield return new WaitForSeconds(secondSound.length);
            playSong(playedSong);
        }
    }

    private void playSong(int playSong){
        if(playedSong == 1){
            audioSource.PlayOneShot(sound);
            playedSong = 2;
        }else if(playedSong == 2){
            audioSource.PlayOneShot(secondSound);
            playedSong = 1;
        }
        StartCoroutine("WaitEndSong", playedSong);
    }
}