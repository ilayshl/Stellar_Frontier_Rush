using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for the homing missile.
/// </summary>
public class Missile : Bullet
{
    [SerializeField] private float rotationSpeed;
    [Header("Hitbox Settings")]
    [SerializeField] private Vector2 boxSize = new Vector2(4, 10);
    [SerializeField] private int distanceFromPosition = 3;
    [Header("Additional Settings")]
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private AudioClip explosionSound;

    private bool hasTarget = false;
    private const float HAS_TARGET_MULT = 1.5f;

    protected override void Start()
    {
        base.Start();
        StartCoroutine("CheckRaycast");
    }

    protected override void Update()
    {
        if (hasTarget)
        {
            transform.position += transform.up * Time.deltaTime * moveSpeed * HAS_TARGET_MULT;
        }
        else
        {
            transform.position += transform.up * Time.deltaTime * moveSpeed;
        }
    }

    /// <summary>
    /// Checks for any enemies in a rectangle in front of the missile.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckRaycast()
    {
        RaycastHit2D hit = new();
        while (hit.collider == null)
        {
            Vector2 position = transform.position;
            float angle = transform.localRotation.z;
            hit = Physics2D.BoxCast(position, boxSize, angle, transform.up, distanceFromPosition, LayerMask.GetMask("Enemy"));
            yield return new WaitForSeconds(0.20f);
        }
        StartCoroutine("MoveTowardsTarget", hit.collider.transform);
        hasTarget = true;
    }

    /// <summary>
    /// Moves towards the set target.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private IEnumerator MoveTowardsTarget(Transform target)
    {
        while (target != null && Vector2.Distance(this.transform.position, target.position) > 0.05f)
        {
            float angle = RotateTowardsTarget(this.transform, target);
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        if (target == null)
        {
            StartCoroutine("CheckRaycast");
            hasTarget = false;
        }
    }

    /// <summary>
    /// Moves horizontally towards the target.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private float RotateTowardsTarget(Transform start, Transform target)
    {
        Vector2 direction = target.position - start.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return angle;
    }

    //Explodes on destroy.
    void OnDestroy()
    {
        var particle = Instantiate(explosionParticles.gameObject, transform.position, Quaternion.identity);
        Destroy(particle, 2f);
        SoundManager.PlaySound(explosionSound);
    }

}
