using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets instance;

    public static SoundAssets i
    {
        get
        {
            if (instance == null) instance = Instantiate(Resources.Load<SoundAssets>("SoundAssets"));
            return instance;
        }
    }

    public SoundFXClip[] soundFXClipArray;

    [System.Serializable]
    public class SoundFXClip
    {
        public SoundManager.SoundFX sound;
        public AudioClip audioClip;
    }
}
