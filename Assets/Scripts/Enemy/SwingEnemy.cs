using UnityEngine;

public class SwingEnemy : Enemy
{
    private const float EDGEX = 7.5f;
    private const int BERSERK_INITIATE_HP = 1;
 
    void Update()
    {
        Move();
        CheckForScreenEdges();
    }

    //Changes transform.position by the direction and moveSpeed.
    private void Move()
    {
        transform.position += new Vector3(moveDir * moveSpeed * Time.deltaTime, 0, 0);
    }
    
    //Changes direction when touching screen edges.
    private void CheckForScreenEdges()
    {
        if ((transform.position.x >= EDGEX && moveDir > 0)
        || (transform.position.x <= -EDGEX && moveDir < 0))
        {
            RowDown();
        }
    }

    //Moves 1 row down (closer to the player) and changes the movement direction.
    private void RowDown()
    {
        SetDirection(-1);
        transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0);
    }

    /// <summary>
    /// Makes the enemy take x damage, if dead, triggers death events.
    /// </summary>
    /// <param name="dmg"></param>
    public override void OnHit(int dmg)
    {
        animator.SetTrigger("onHit");
        base.OnHit(dmg);
        if (hp.currentHP == BERSERK_INITIATE_HP)
        {
            if (sr.color == Color.green) //If enemy is Berserker
            {
                float berserkerBonus = 1.5f;
                moveSpeed *= berserkerBonus;
            }
        }
    }

    protected override void OnDeath()
    {
        if(Random.Range(0, 100)<=33)
        {
            var pickupSpawned = enemyManager.SpawnPickup(transform.position);
            Pickup pickup = pickupSpawned.GetComponent<Pickup>();
            pickup.StartCoroutine(pickup.RandomizePickup());
        }
        base.OnDeath();
    }
}
