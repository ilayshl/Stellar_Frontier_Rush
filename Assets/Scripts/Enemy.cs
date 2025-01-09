using UnityEngine;

/// <summary>
/// Responsible for enemies in the game and dropping pickups on death.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private GameObject[] pickups;
    private float xEdge = 7.5f;
    private int moveDir = 1;
    private HitPoints hp;
    private WaveManager waveManager;
    private SpriteRenderer sr;

    private void Awake()
    {
        hp = GetComponent<HitPoints>();
        waveManager = GetComponentInParent<WaveManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //IncreaseDifficulty();
        Move();
        CheckForScreenEdges();
    }

    private void Move()
    {
        transform.position += new Vector3(moveDir * moveSpeed * Time.deltaTime, 0, 0);
    }
    public void SetSpeed(float addition) {
        moveSpeed+=addition;
    }

    private void CheckForScreenEdges()
    {
        if ((transform.position.x >= xEdge && moveDir > 0)
        || (transform.position.x <= -xEdge && moveDir < 0))
        {
            RowDown();
        }
    }

    private void RowDown()
    {
        moveDir *= -1;
        transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0);
    }
    
    public void OnHit(int dmg)
    {
        hp.LoseHealth(dmg);
        if (hp.IsDead())
        {
            waveManager.EnemyKilled();
            Destroy(gameObject);
        }
        if(hp.Health() == 1){
        if(sr.color == Color.green){
            moveSpeed*=2;
        }
        }
    }

    private void OnDestroy()
    {
        if (Random.Range(0, 100) <= 33)
        {
            Debug.Log("Spawned a pickup!");
            //Instantiate(pickups[Random.Range(0, pickups.Length)]);
        }
        if (sr.color == Color.black)
        {
            Debug.Log("allahu akbnar");
        }
    }
}
