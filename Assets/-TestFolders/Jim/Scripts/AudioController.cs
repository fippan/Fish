using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private AudioMixerGroup mixerGroup;
    private AudioSource[] audioSources;

    private void Start()
    {
        audioSources = new AudioSource[sounds.Length];
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject(sounds[i].name);
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            audioSources[i] = go.AddComponent<AudioSource>();
            audioSources[i].playOnAwake = false;
            audioSources[i].clip = sounds[i].audioClip;
            audioSources[i].minDistance = sounds[i].minDistance;
            audioSources[i].maxDistance = sounds[i].maxDistance;
            audioSources[i].spatialBlend = sounds[i].spatialBlend;
            audioSources[i].outputAudioMixerGroup = mixerGroup;
        }
    }

    public void Play(string soundName, Vector3 worldPoint)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (sounds[i].name == soundName)
            {
                audioSources[i].transform.position = worldPoint;
                SetVolumeAndPitch(i);
                audioSources[i].Play();
            }
        }
    }

    public void Stop(string soundName)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (sounds[i].name == soundName)
            {
                audioSources[i].Stop();
            }
        }
    }

    public void PlayOneShot(string soundName, Vector3 worldPoint)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (sounds[i].name == soundName)
            {
                audioSources[i].transform.position = worldPoint;
                SetVolumeAndPitch(i);
                audioSources[i].PlayOneShot(audioSources[i].clip);
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
