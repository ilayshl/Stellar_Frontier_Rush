using UnityEngine;

/// <summary>
/// Responsible for the player's movement and shooting commands.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    private Shoot shoot;
    private float fireSpeed = 0.5f;
    private float lastShotTime;

    private void Awake() {
        shoot = GetComponent<Shoot>();
    }

    void Update()
    {
        MoveToMouse();
        CheckForFiring();
    }

    private void MoveToMouse(){
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPosition = transform.position;
        transform.position =
        Vector2.MoveTowards(currentPosition, mousePosition,  moveSpeed * Time.deltaTime);
    }

    private void CheckForFiring(){
        if(Input.GetMouseButton(0)){
            if(Time.time > lastShotTime + fireSpeed){
            lastShotTime = Time.time;
            shoot.ShootBullet(transform.position, transform.rotation);
            
            }
        }
    }

    public void IncreaseSpeed(float increase) {
        moveSpeed+=increase;
    }

    //Lower value = lower interval between shots
    public void IncreaseFireSpeed(float increase) {
        fireSpeed-=increase;
    }
}
