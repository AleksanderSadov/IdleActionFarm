using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool playOnAwake = false;

    [SerializeField] private List<AudioClip> clipsList;
    [SerializeField] private AudioSource audioSource;

    public static MusicManager Instance;

    private bool isPlayOnRepeat = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (playOnAwake)
        {
            isPlayOnRepeat = true;
        }
    }

    private void Update()
    {
        if (isPlayOnRepeat && !audioSource.isPlaying)
        {
            PlayRandomClipOnce();
        }
    }

    public void PlayRandomClipOnce()
    {
        audioSource.clip = clipsList[Random.Range(0, clipsList.Count)];
        audioSource.Play();
    }

    public void PlayRandomclipsOnRepeat()
    {
        isPlayOnRepeat = true;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
