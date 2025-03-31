using UnityEngine;

/// <summary>
/// Responsible for the basic bullet's movement.
/// </summary>
public class Bullet : Projectile
{
    [SerializeField] private Vector3 trajectory;

    protected override void Update()
    {
        SetTrajectory(trajectory);
    }

    //Sets the movement direction of the object.
    private void SetTrajectory(Vector3 direction)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    protected override void OnHit(Vector2 position)
    {
        
    }
}
