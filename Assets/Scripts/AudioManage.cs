using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
    public static AudioClip bounceBall, breakWall;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        bounceBall = Resources.Load<AudioClip>("BounceBall");
        breakWall = Resources.Load<AudioClip>("Break");
        audioSource = GetComponent<AudioSource>();
    }
    public static void SetPitch(float changepitch)
    {
        audioSource.pitch += changepitch;
    }
    public static void ResetPitch(float changepitch)
    {
        audioSource.pitch = changepitch;
    }
    public static void PlaySound(int clip)
    {
        switch (clip)
        {
            case 1:
                audioSource.PlayOneShot(bounceBall);
                break;
            case 2:
                audioSource.PlayOneShot(breakWall);
                break;

        }
    }
}
