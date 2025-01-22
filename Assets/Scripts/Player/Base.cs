using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsible for the player's base. With each enemy striking the base, hp will be lost, until reaching 0.
/// </summary>
public class Base : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioClip[] impactSounds;

    private HitPoints hp;
    private SpriteRenderer sr;
    private SoundManager soundManager;

    private const int initialHP = 10;

    private void Awake()
    {
        hp = GetComponent<HitPoints>();
        sr = GetComponent<SpriteRenderer>();
        soundManager = GetComponent<SoundManager>();
    }

    // Damages the object upon collision.
    private void OnTriggerEnter2D(Collider2D other)
    {
        int randomIndex = Random.Range(0, impactSounds.Length);
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            ChangeHealth(-enemy.Damage());
            enemy.OnHit(100);
            soundManager.PlaySound(impactSounds[randomIndex]);
        }
        else if (other.TryGetComponent<Meteor>(out Meteor meteor))
        {
            ChangeHealth(-meteor.Damage());
            meteor.OnHit(100);
            soundManager.PlaySound(impactSounds[randomIndex]);
        }
        if (hp.IsDead())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// Changes and updates the value of HP by the amount given- either negative (decreases hp) or positive (increases hp).
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth(int amount)
    {
        if (amount > 0)
        {
            hp.GainHealth(amount);
        }
        else if (amount < 0)
        {
            hp.LoseHealth(amount);
        }
        else
        {
            Debug.LogError("Healing for a value of 0. Check your code.");
        }
        UpdateHealthText(hp.Health());
    }

    //Updates the text UI of Health to match the given value.
    private void UpdateHealthText(int value)
    {
        uiManager.SetText(0, value.ToString());
    }
}
