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

    private float RotateTowardsTarget(Transform start, Transform target)
    {
        Vector2 direction = target.position - start.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return angle;
    }

    protected override void OnHit(Vector2 position)
    {
        var particle = Instantiate(explosionParticles.gameObject, position, Quaternion.identity);
        Destroy(particle, 2f);
    }

}
