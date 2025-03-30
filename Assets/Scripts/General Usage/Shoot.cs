using UnityEngine;

/// <summary>
///Responsible for shooting- both enemies and the player.
/// </summary>
public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private AudioClip[] shootingSounds;
    [SerializeField] private Missile missile;

    /// <summary>
    /// Returns read-only of the length of the array of bullets.
    /// </summary>
    /// <returns></returns>
    public int BulletTypes()
    {
        return bullet.Length;
    }

    /// <summary>
    /// Creates new basic bullet. --made for enemies that can shoot.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public void ShootBullet(Vector3 position)
    {
        var newBullet = Instantiate(bullet[0], position, bullet[0].transform.localRotation);
        PlayShootingSound(0);
    }

    /// <summary>
    /// Creates new specified type of bullet and sets its damage.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public void ShootBullet(Vector3 position, int index)
    {
        int cappedBulletType = Mathf.Min(index, bullet.Length);
        var newBullet = Instantiate(bullet[cappedBulletType], position, bullet[cappedBulletType].transform.localRotation);
        if (newBullet.TryGetComponent<Bullet>(out Bullet bulletScript))
        {
            bulletScript.SetDamage(index + 1); //Index starts at 0, therefore +1.
        }
        PlayShootingSound(index);
    }

    //To be continued, shooting missile
    public void ShootMissile(Vector3 position)
    {
        Instantiate(missile, position, missile.transform.localRotation);
        PlayShootingSound(0);
    }

    //Plays sound of a specific index in shootingSounds.
    private void PlayShootingSound(int index)
    {
        if (TryGetComponent<SoundManager>(out SoundManager soundManager)){
            soundManager.PlaySound(shootingSounds[index]);
        }
    }
}
