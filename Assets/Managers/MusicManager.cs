using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField]
    private List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource audioSource;

    void Awake()
    {
        // Ensure that there's only one instance of this class in the scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();

        PlayRandomMusic();
    }

    private void PlayRandomMusic()
    {
        if (audioClips.Count == 0)
        {
            Debug.LogWarning("MusicManager: No audio clips available to play.");
            return;
        }

        int randomIndex = Random.Range(0, audioClips.Count);
        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
        StartCoroutine(WaitForMusicEnd());
    }

    private IEnumerator WaitForMusicEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        PlayRandomMusic();
    }

    // Optional method to add more clips at runtime
    public void AddClip(AudioClip clip)
    {
        if (!audioClips.Contains(clip))
        {
            audioClips.Add(clip);
        }
    }
}
