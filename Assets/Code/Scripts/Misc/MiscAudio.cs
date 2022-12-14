using UnityEngine;
using UnityEngine.Audio;

public static class MiscAudio
{
    public static void PlayClipAtPoint(
        AudioClip clip,
        Vector3 position,
        float volume = 1.0f,
        float pitchRange = 0.0f,
        AudioMixerGroup group = null
    )
    {
        if (clip == null)
        {
            return;
        }
                
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource) gameObject.AddComponent(typeof(AudioSource));

        if (group != null)
        {
            audioSource.outputAudioMixerGroup = group;
        }

        if (pitchRange != 0)
        {
            audioSource.pitch = Random.Range(audioSource.pitch - pitchRange, audioSource.pitch + pitchRange);
        }

        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.Play();

        Object.Destroy(gameObject, clip.length *
            (Time.timeScale < 0.009999999776482582 ? 0.01f : Time.timeScale));
    }
}
