using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Audio[] sounds;

    public GameObject currentTarget;

    /// <summary>
    /// If a audiomanager already exists in the scene, this one will be destroyed.
    /// Instantiates all the sounds in a foreach loop.
    /// </summary>
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Audio s in sounds)
        {
            //If there is a target to put the sound effect on, use that target. Else use the audiomanager as target.
            if (s.target.gameObject != null)
            {
                s.source = s.target.gameObject.AddComponent<AudioSource>();
            }
            else
            {
                s.source = gameObject.AddComponent<AudioSource>();
            }
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            //How far the sound will reach.
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;

            var newMixerGroup = s.mixerGroup;

            if (newMixerGroup == null)
            {
                newMixerGroup = mixerGroup;
            }

            s.source.outputAudioMixerGroup = newMixerGroup;
        }
    }

    void Start()
    {
        //FindObjectOfType<AudioManager>().Play("name of song here");
    }

    /// <summary>
    /// This function is called from other scripts in order to play the sound clips.
    /// </summary>
    //public void Play(string sound)
    //{
    //    Audio s = Array.Find(sounds, item => item.name == sound);
    //    if (s == null)
    //    {
    //        Debug.LogWarning("Sound: " + name + " not found!");
    //        return;
    //    }
    //    if (currentTarget != null)
    //    {
    //        s.target = TransformTarget(currentTarget);
    //        s.source = currentTarget.GetComponent<AudioSource>();
    //        s.source = s.target.gameObject.AddComponent<AudioSource>();
    //    }
    //    s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
    //    s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
    //    s.source.spatialBlend = s.spatialBlend;
    //    s.source.Play();
    //}

    //public void PlayTest(GameObject gameObject)
    //{
    //    gameObject.GetComponent<AudioSource>().Play();
    //}

    //public GameObject TransformTarget(GameObject target)
    //{

    //    target.gameObject.AddComponent<AudioSource>();
    //    target.gameObject.GetComponent<AudioSource>().clip = sounds[0].clip;
    //    currentTarget = target;
    //    return currentTarget;
    //}

}