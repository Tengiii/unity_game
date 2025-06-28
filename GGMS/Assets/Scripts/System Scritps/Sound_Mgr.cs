using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType { 
    SLASH,
    FIREBALL,
    LIGHTNING,
    FOOTSTEP,
    DASH,
    HURT,
    SWORD_HIT,
}

[RequireComponent(typeof(AudioSource))]
public class Sound_Mgr : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static Sound_Mgr Instance;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume)
    {
        Instance.audioSource.PlayOneShot(Instance.soundList[(int)sound], volume);
    }

}
