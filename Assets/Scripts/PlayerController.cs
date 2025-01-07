using UnityEngine;

/// <summary>
/// Responsible for the player's movement and shooting commands.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    private Shoot shoot;

    private void Awake() {
        shoot = GetComponent<Shoot>();
    }

    void Start()
    {
        Debug.Log("OK");
    }

    void Update()
    {
        MoveToMouse();
        CheckForFiring();
    }

    void MoveToMouse(){
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPosition = transform.position;
        transform.position =
        Vector2.MoveTowards(currentPosition, mousePosition,  moveSpeed * Time.deltaTime);
    }

    void CheckForFiring(){
        if(Input.GetMouseButtonDown(0)){
            shoot.ShootBullet(transform.position, transform.rotation);
        }
    }
}
