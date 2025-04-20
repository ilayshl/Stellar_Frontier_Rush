using System; //idk why it's dark gray, it is used in the script
using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for the player's movement and shooting commands.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private Transform rightCannon, leftCannon;
    private float shootInterval = 0.7f;
    private float lastShotTime;
    private int bulletType = 0;
    private bool lastShotFromRight = false;

    private Shoot shoot;
    private UIManager uiManager;
    private Base playerBase;
    private Animator animator;

    private void Awake()
    {
        shoot = GetComponent<Shoot>();
        uiManager = FindFirstObjectByType<UIManager>();
        playerBase = FindFirstObjectByType<Base>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Debug.Log($"Debug commands:\nQ: Add to wave count\nA: Clear all living enemies\nS: Spawn a random pickup\nD: Damage player");   
    }

    void Update()
    {
        MoveToMouse();
        CheckForFiring();
    }

    //Moves the object towards the cursor location with moveSpeed.
    private void MoveToMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPosition = transform.position;
        transform.position =
        Vector2.MoveTowards(currentPosition, mousePosition, moveSpeed * Time.deltaTime);
    }

    //Checks if left mouse button is pressed to shoot if possible by shootInterval.
    private void CheckForFiring()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time > lastShotTime + shootInterval)
            {
                lastShotTime = Time.time;
                shoot.ShootBullet(GetActiveCannon(), bulletType);
                animator.SetTrigger("isShooting");
                lastShotFromRight=!lastShotFromRight;
            }
        }

       //For Missiles
       if (Input.GetMouseButtonDown(1))
        {
            shoot.ShootMissile(GetActiveCannon());
            animator.SetTrigger("isShooting");
            lastShotFromRight=!lastShotFromRight;
        }
    }

    private IEnumerator Shoot()
    {
        //while the pressed key is MouseButton 0, shoot.
        //yield return shootInterval;
        //outside the while scope- activeCoroutine = null;
        yield return null;
    }

    //Returns which cannon to shot from based on the last cannon that shot.
    private Vector3 GetActiveCannon()
    {
        if (lastShotFromRight)
        {
            return leftCannon.position;
        }
        else
        {
            return rightCannon.position;
        }
    }

    /// <summary>
    /// Increases moveSpeed by the amount given.
    /// </summary>
    /// <param name="increase"></param>
    public void IncreaseSpeed(float increase)
    {
        moveSpeed += increase;
        uiManager.SetText(3, moveSpeed.ToString());
    }

    /// <summary>
    /// Percentage of how faster the player will shoot; lower shootInterval = faster fire rate.
    /// </summary>
    /// <param name="increase"></param>
    public void IncreaseFireRate(float increase)
    {
        float bonusPercentage = 1 - (increase / 100);
        shootInterval *= bonusPercentage;
        shootInterval = Mathf.Max(0.1f, shootInterval);
        float dps = 1 / shootInterval;
        uiManager.SetText(2, System.Math.Round(dps, 2).ToString());
    }

    /// <summary>
    /// Increases the damage impact of the object's bullets.
    /// </summary>
    /// <param name="increase"></param>
    public void IncreaseDamage(int increase)
    {
        bulletType += increase;
        bulletType = Mathf.Min(bulletType, shoot.BulletTypes() - 1);
        uiManager.SetText(1, (bulletType + 1).ToString());
    }

    /// <summary>
    /// Increases Health amount by an int, but can't surpass the initial Health amount.
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseHealth(int value)
    {
        playerBase.ChangeHealth(value);
    }

    public int Damage()
    {
        return bulletType+1;
    }
}
