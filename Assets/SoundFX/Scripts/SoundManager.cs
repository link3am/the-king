using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    //To handle multiple sounds
   public enum SoundFX
    {
        PlayerMove,
        PlayerShoot,
        PlayerHit,
        EnemyShoot,
        EnemyHit,
        EnemyDie,
        Reload,
    }

    public static void PlaySound(SoundFX sound)
    {
        GameObject soundGameObject = new GameObject("SoundFX");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudio(sound));
    }

    private static AudioClip GetAudio(SoundFX sound)
    {
        foreach (SoundAssets.SoundFXClip soundFxClip in SoundAssets.i.soundFXClipArray)
        {
            if(soundFxClip.sound == sound)
            {
                return soundFxClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "not found");
        return null;
    }

}
