using UnityEngine;

/// <summary>
/// Holds the AudioSource component and ChangePitch method to avoid repeating the same sounds.
/// </summary>
public class SoundManager : MonoBehaviour
{
    private static SoundManager Instance;

    private AudioSource source;

    private const int DEFAULT_PITCH = 1;

    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the given sound once with a randomized pitch to prevent sound fatigue.
    /// </summary>
    /// <param name="sound"></param>
    public static void PlaySound(AudioClip sound, bool newPitch = false)
    {
        if (newPitch)
        {
            Instance.ResetPitch();
            Instance.ChangePitch();
        }
        else
        {
            Instance.ResetPitch();
        }
        Instance.source.PlayOneShot(sound);
    }

    //Resets the pitch back to its original value.
    private void ResetPitch()
    {
        source.pitch = DEFAULT_PITCH;
    }

    //Changes the pitch by an offset of 0.1 in either direction.
    private void ChangePitch()
    {
        float offset = Random.Range(-0.15f, 0.15f);
        source.pitch += offset;
    }
}
