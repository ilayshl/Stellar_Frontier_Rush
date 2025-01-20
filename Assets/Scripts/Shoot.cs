using UnityEngine;

/// <summary>
///Responsible for shooting- both enemies and the player.
/// </summary>
public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject[] bullet;

    //Returns read-only of current bullet type int from the array of bullets.
    public int BulletTypes()
    {
        return bullet.Length;
    }

    //Creates new basic bullet.
    public void ShootBullet(Vector3 position, Quaternion direction)
    {
        var newBullet = Instantiate(bullet[0], position, direction);
    }

    //Creates new specified type of bullet and sets its damage.
    public void ShootBullet(int index, Vector3 position, Quaternion direction)
    {
        var newBullet = Instantiate(bullet[Mathf.Min(index, bullet.Length)], position, direction);
        if (newBullet.TryGetComponent<Bullet>(out Bullet bulletScript))
        {
            bulletScript.SetDamage(index);
        }
    }
}
