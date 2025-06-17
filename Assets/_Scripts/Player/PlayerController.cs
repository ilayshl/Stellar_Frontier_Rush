using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for the player's movement and shooting commands.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public int damageCap { get => shoot.bullet.Length * 2; }
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private ParticleSystem rightCannon, leftCannon;
    private Coroutine activeShooting;
    private bool lastShotFromRight;

    private Shoot shoot;
    private Animator animator;

    private void Awake()
    {
        shoot = GetComponent<Shoot>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Debug.Log($"Debug commands:\nQ: Add to wave count\nA: Clear all living enemies\nS: Spawn a random pickup\nD: Damage player");
        Cursor.visible = false;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        if (GameManager.Instance.state == GameState.Active)
        {   
        MoveToMouse();
        CheckForFiring();
        }
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
            if (activeShooting == null)
            {
                float fireInterval = PlayerManager.Instance.GetStatValue(StatType.FireRate);
                activeShooting = StartCoroutine(Shoot(fireInterval));
            }
        }

        //For Missiles
        if (Input.GetMouseButtonDown(1))
        {
            if (PlayerManager.Instance.GetStatValue(StatType.Missile) > 0)
            {
                ShootMissile();
            }
        }
    }

    /// <summary>
    /// Shoots a bullet, then resets activeShooting so it could be fired again.
    /// </summary>
    /// <param name="timeInterval"></param>
    /// <returns></returns>
    private IEnumerator Shoot(float timeInterval)
    {
        var activeCannon = GetActiveCannon();
        int maxDamageBullet = Math.Min(shoot.bullet.Length, (int)PlayerManager.Instance.GetStatValue(StatType.Damage));
        shoot.ShootBullet(activeCannon.transform.position, maxDamageBullet, true);
        activeCannon.Play();
        animator.SetTrigger("isShooting");
        lastShotFromRight = !lastShotFromRight;
        if (PlayerManager.Instance.GetStatValue(StatType.Damage) > shoot.bullet.Length)
        {
            int newBulletDamage = (int)PlayerManager.Instance.GetStatValue(StatType.Damage) - shoot.bullet.Length;
            int maxNewDamageBullet = Math.Min(shoot.bullet.Length, newBulletDamage);
            var newActiveCannon = GetActiveCannon();
            shoot.ShootBullet(newActiveCannon.transform.position, maxNewDamageBullet, false);
        }
        yield return new WaitForSeconds(timeInterval);
        activeShooting = null;
    }

    /// <summary>
    /// Shoots a missle in the active cannon if there's ammo
    /// </summary>
    private void ShootMissile()
    {
        var activeCannon = GetActiveCannon();
        shoot.ShootMissile(activeCannon.transform.position, (int)PlayerManager.Instance.GetStatValue(StatType.Damage));
        activeCannon.Play();
        animator.SetTrigger("isShooting");
        PlayerManager.Instance.ChangeStat(StatType.Missile, -1);
        lastShotFromRight = !lastShotFromRight;
    }

    //Returns which cannon to shot from based on the last cannon that shot.
    private ParticleSystem GetActiveCannon()
    {
        if (lastShotFromRight)
        {
            return leftCannon;
        }
        else
        {
            return rightCannon;
        }
    }

    /* /// <summary>
    /// Increases Health amount by an int, but can't surpass the initial Health amount.
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseHealth(int value)
    {
        playerBase.ChangeHealth(value);
    }

    /// <summary>
    /// Increases the damage impact of the object's bullets.
    /// </summary>
    /// <param name="increase"></param>
    public void IncreaseDamage(int increase)
    {
        damage += increase;
        damage = Mathf.Min(damage, shoot.BulletTypes());
        uiManager.SetText((int)StatType.Damage, damage.ToString());
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
        uiManager.SetText((int)StatType.FireRate, Math.Round(dps, 2).ToString());
    }

    /// <summary>
    /// Increases moveSpeed by the amount given.
    /// </summary>
    /// <param name="increase"></param>
    public void IncreaseSpeed(float increase)
    {
        moveSpeed += increase;
        uiManager.SetText((int)StatType.MoveSpeed, moveSpeed.ToString());
    }

    /// <summary>
    /// Increases the missile ammo by the amount given.
    /// </summary>
    /// <param name="value"></param>
    public void ChangeMissileAmmo(int value)
    {
        missileAmmo += value;
        uiManager.SetText((int)StatType.Missile, missileAmmo.ToString());
    } */
}
