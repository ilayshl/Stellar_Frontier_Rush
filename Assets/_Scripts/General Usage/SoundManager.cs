using UnityEngine;

/// <summary>
/// Holds the AudioSource component and ChangePitch method to avoid repeating the same sounds.
/// </summary>
public class SoundManager : MonoBehaviour
{
    private const int DEFAULT_PITCH = 1;
    private const float PITCH_OFFSET = .25f;
    private static SoundManager Instance;
    private AudioSource source;

    private void Awake()
    {
         if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
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
        float offset = Random.Range(-PITCH_OFFSET, PITCH_OFFSET);
        source.pitch += offset;
    }
}
