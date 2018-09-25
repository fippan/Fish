using UnityEngine;
using UnityEngine.Audio;

public class WeaponAudioManager : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    public Sound[] sounds;

    private AudioSource[] audioSources;

    private void Start()
    {
        audioSources = new AudioSource[sounds.Length];
        for (int i = 0; i < sounds.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].clip = sounds[i].audioClip;
            audioSources[i].minDistance = sounds[i].minDistance;
            audioSources[i].maxDistance = sounds[i].maxDistance;
            audioSources[i].spatialBlend = sounds[i].spatialBlend;
            audioSources[i].outputAudioMixerGroup = mixerGroup;
        }
    }

    public void PlayOneShot(string name)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (sounds[i].name == name)
            {
                SetVolumeAndPitch(i);
                audioSources[i].PlayOneShot(audioSources[i].clip);
            }
        }
    }

    public void Play(string name, ulong delay)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (sounds[i].name == name)
            {
                SetVolumeAndPitch(i);
                audioSources[i].Play(delay);
            }
        }
    }

    private void SetVolumeAndPitch(int i)
    {
        audioSources[i].volume = sounds[i].volume * (1f + Random.Range(
            -sounds[i].volumeVariance * .5f,
            sounds[i].volumeVariance * .5f));
        audioSources[i].pitch = sounds[i].pitch * (1f + Random.Range(
            -sounds[i].pitchVariance * .5f,
            sounds[i].pitchVariance * .5f));
    }
}


[System.Serializable]
public struct Sound
{
    public string name;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float volumeVariance;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float pitchVariance;
    [Range(0f, 1f)]
    public float spatialBlend;

    public float minDistance;
    public float maxDistance;
}
