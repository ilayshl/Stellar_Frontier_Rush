using UnityEngine;

/// <summary>
///Responsible for shooting- both enemies and the player.
/// </summary>
public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    public void ShootBullet(Vector3 position, Quaternion direction) {
        var newBullet = Instantiate(bullet, position, direction);
    }

    public void ShootBullet(Vector3 position, Quaternion direction, int dmg){
        var newBullet = Instantiate(bullet, position, direction);
        if(TryGetComponent<Bullet>(out Bullet bulletScript)) {
            bulletScript.SetDamage(dmg);
        }
    }
}
