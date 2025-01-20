using UnityEngine;

/// <summary>
/// Responsible for enemies in the game and dropping pickups on death.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    private float xEdge = 7.5f;
    private int moveDir = 1;
    private HitPoints hp;
    private WaveManager waveManager;
    private SpriteRenderer sr;
    private Animator animator;

    private void Awake()
    {
        hp = GetComponent<HitPoints>();
        waveManager = GetComponentInParent<WaveManager>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        CheckForScreenEdges();
    }

    //Changes transform.position by the direction and moveSpeed.
    private void Move()
    {
        transform.position += new Vector3(moveDir * moveSpeed * Time.deltaTime, 0, 0);
    }

    //Sets moveSpeed of the enemy.
    public void SetSpeed(float addition)
    {
        moveSpeed += addition;
    }

    //Changes direction when touching screen edges.
    private void CheckForScreenEdges()
    {
        if ((transform.position.x >= xEdge && moveDir > 0)
        || (transform.position.x <= -xEdge && moveDir < 0))
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

    //Makes the enemy take x damage, if dead, triggers death events.
    public void OnHit(int dmg)
    {
        hp.LoseHealth(dmg);
        animator.SetTrigger("onHit");
        if (hp.IsDead())
        {
            waveManager.EnemyKilled(this.transform);
            Destroy(gameObject);
        }
        if (hp.Health() == 1)
        {
            if (sr.color == Color.green)
            {
                moveSpeed *= 2;
            }
        }
    }

    //Sets the direction of the object by an int value- negative is left, positive is right.
    public void SetDirection(int direction){
        moveDir *= direction;
    }
}
