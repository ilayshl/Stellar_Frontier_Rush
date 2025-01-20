using System;
using UnityEngine;

/// <summary>
/// Responsible for the player's movement and shooting commands.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    private Shoot shoot;
    private float shootInterval = 0.7f;
    private float lastShotTime;
    int bulletType = 0;

    private UIManager uiManager;

    private void Awake()
    {
        shoot = GetComponent<Shoot>();
        uiManager = FindFirstObjectByType<UIManager>();
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
                shoot.ShootBullet(bulletType, transform.position, transform.rotation);
            }
        }
    }

    //Increases moveSpeed.
    public void IncreaseSpeed(float increase)
    {
        moveSpeed += increase;
        uiManager.SetText(3, moveSpeed.ToString());
    }

    //Percentage of how faster the player will shoot; lower shootInterval = faster fire rate.
    public void IncreaseFireRate(float increase)
    {
        shootInterval *= 1 - (increase / 100);
        if (shootInterval < 0.1) { shootInterval = 0.1f; }
        float dps = 1 / shootInterval;
        uiManager.SetText(2, System.Math.Round(dps, 2).ToString());
    }

    //Increases the damage impact of the object's bullets.
    public void IncreaseDamage(int increase)
    {
        bulletType += increase;
        bulletType = Mathf.Min(bulletType, shoot.BulletTypes() - 1);
        uiManager.SetText(1, (bulletType + 1).ToString());
    }
}
