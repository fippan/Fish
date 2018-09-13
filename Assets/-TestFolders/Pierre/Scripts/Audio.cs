using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{

    public GameObject target;

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = .75f;
    [Range(0f, 1f)]
    public float volumeVariance = .1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float pitchVariance = .1f;
    [Range(0f, 1f)]
    public float spatialBlend = 0f;

    public float minDistance = 1f;
    public float maxDistance = 500f;

    public bool loop = false;

    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;
}