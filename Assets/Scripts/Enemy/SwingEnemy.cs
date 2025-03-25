using UnityEngine;

public class SwingEnemy : Enemy
{
    private Animator _animator;

    private const float EDGEX = 7.5f;
 
    void Update()
    {
        Move();
        CheckForScreenEdges();
    }

    /*/// <summary>
    /// Returns the damage of the enemy.
    /// </summary>
    /// <returns></returns>
    public int Damage()
    {
        return dmg;
    }*/

    /*/// <summary>
    /// Sets the direction of the object by an int value- negative is left, positive is right.
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(int direction)
    {
        moveDir *= direction;
    }*/

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
        if (hp.currentHP == 1)
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
        if(Random.Range(0, 100)<=25)
        {
            enemyManager.SpawnPickup(transform);
        }
        base.OnDeath();
    }
}
