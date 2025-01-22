using UnityEngine;

/// <summary>
/// Holds the AudioSource component and ChangePitch method to avoid repeating the same sounds.
/// </summary>
public class SoundManager : MonoBehaviour
{
    private AudioSource source;

    private const int pitchDefault = 1;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the given sound once with a randomized pitch.
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySound(AudioClip sound)
    {
        ResetPitch();
        ChangePitch();
        source.PlayOneShot(sound);

    }

    //Resets the pitch back to its original value.
    private void ResetPitch()
    {
        source.pitch = pitchDefault;
    }

    //Changes the pitch by an offset of 0.1 in either direction.
    private void ChangePitch()
    {
        float offset = Random.Range(-0.1f, 0.1f);
        source.pitch += offset;
    }
}
