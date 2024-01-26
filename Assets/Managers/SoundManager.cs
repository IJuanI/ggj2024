using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] List<AudioClip> sounds = new List<AudioClip>();
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void PlaySound(int indexOfSounds)
    {
        if (indexOfSounds < sounds.Count)
        {
            audioSource.PlayOneShot(sounds[indexOfSounds]);
        }
    }
}
