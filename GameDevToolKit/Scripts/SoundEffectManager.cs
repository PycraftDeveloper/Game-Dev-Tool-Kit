using UnityEngine;
using System.Collections.Generic;

namespace GameDevToolKit
{
    public class SoundEffectManager : MonoBehaviour
    {
        [Tooltip("This globally controls the volume a sound effect will play at. " +
    "This is mixed with the Volume defined in the 'OneShotSetup'. So you would use " +
    "this for example when controling the volume for the game in a settings menu.")]
        [SerializeField]
        [Range(0.0f, 1.0f)] public float SoundEffectVolumeControl = 1.0f;

        public GameObject PlayExtendedOneShot(OneShotSetup Setup)
        {
            GameObject NewAudioSource = new GameObject("SFX_ExtendedOneShot");
            AudioSource ExtendedAudioSourceComponent = NewAudioSource.AddComponent<AudioSource>();
            ExtendedOneShot ExtendedOneShotComponent = NewAudioSource.AddComponent<ExtendedOneShot>();
            AudioClip clip = Setup._AudioClip[Random.Range(0, Setup._AudioClip.Count)];
            ExtendedAudioSourceComponent.clip = clip;
            ExtendedAudioSourceComponent.volume = SoundEffectVolumeControl * Setup.Volume;
            ExtendedAudioSourceComponent.panStereo = Setup.StereoPan;
            ExtendedAudioSourceComponent.pitch = Random.Range(1.0f - Setup.PitchRange, 1.0f + Setup.PitchRange);
            ExtendedAudioSourceComponent.Play();
            ExtendedOneShotComponent.Lifetime = clip.length;
            NewAudioSource.transform.parent = transform;
            return NewAudioSource;
        }
    }

    [System.Serializable]
    public class OneShotSetup
    {
        public List<AudioClip> _AudioClip;
        [Range(0.0f, 1.0f)] public float Volume = 1.0f;
        [Range(-1.0f, 1.0f)] public float StereoPan = 0.0f;
        [Range(0.0f, 0.15f)] public float PitchRange = 0.15f;
    }
}