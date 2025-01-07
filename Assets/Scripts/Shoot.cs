using UnityEngine;

/// <summary>
///Responsible for shooting- both enemies and the player.
/// </summary>
public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    public void ShootBullet(Vector3 position, Quaternion direction){
        Instantiate(bullet, position, direction);
    }
}
