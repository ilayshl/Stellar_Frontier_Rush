using UnityEngine;

/// <summary>
///Responsible for shooting- both enemies and the player.
/// </summary>
public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private AudioClip[] shootingSounds;

    /// <summary>
    /// Returns read-only of current bullet type from the array of bullets.
    /// </summary>
    /// <returns></returns>
    public int BulletTypes()
    {
        return bullet.Length;
    }

    /// <summary>
    /// Creates new basic bullet.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public void ShootBullet(Vector3 position, Quaternion direction)
    {
        var newBullet = Instantiate(bullet[0], position, direction);
    }

    /// <summary>
    /// Creates new specified type of bullet and sets its damage.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    public void ShootBullet(int index, Vector3 position, Quaternion direction)
    {
        var newBullet = Instantiate(bullet[Mathf.Min(index, bullet.Length)], position, direction);
        if (newBullet.TryGetComponent<Bullet>(out Bullet bulletScript))
        {
            bulletScript.SetDamage(index+1); //Index starts at 0, therefore +1.
        }
        if(TryGetComponent<SoundManager>(out SoundManager soundManager))
        {
            soundManager.PlaySound(shootingSounds[index]);
        }
    }
}
