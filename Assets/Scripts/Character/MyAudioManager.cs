using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource MainSource;

    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip keyPickUpSound;
    [SerializeField] AudioClip MedKitPickUpSound;
    [SerializeField] AudioClip HandGunShot;
    [SerializeField] AudioClip LaserGunShot;

    public bool GetPlaying()
    {
        return MainSource.isPlaying;
    }

    public void PlayHurtSound()
    {
        MainSource.PlayOneShot(hurtSound, 0.5f);
    }
    public void PlayKeyPickUpSound()
    {
        MainSource.PlayOneShot(keyPickUpSound, 0.8f);
    }

    public void PlayMedKitPickUpSound()
    {
        MainSource.PlayOneShot(MedKitPickUpSound, 0.3f);
    }

    public void PlayHandGunShootSound()
    {
        MainSource.PlayOneShot(HandGunShot, 0.2f);
    }

    public void PlayLaserGunShootSound()
    {
        MainSource.PlayOneShot(LaserGunShot, 0.2f);
    }

}
