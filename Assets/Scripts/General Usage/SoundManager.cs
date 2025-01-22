using UnityEngine;

/// <summary>
/// Holds the AudioSource component.
/// </summary>
public class SoundManager : MonoBehaviour
{
    private AudioSource source;

    private const int pitchDefault = 1;

    private void Awake() {
    source = GetComponent<AudioSource>();    
    }

    public void PlaySound(AudioClip sound){
        ResetPitch();
        ChangePitch();
        source.PlayOneShot(sound);

    }

    private void ResetPitch(){
        source.pitch=pitchDefault;
    }

    private void ChangePitch(){
        float offset = Random.Range(-0.1f, 0.1f);
        source.pitch+=offset;
    }
}
