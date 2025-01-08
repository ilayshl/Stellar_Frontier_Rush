using UnityEngine;

/// <summary>
/// Responsible for enemies in the game and dropping pickups on death.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private GameObject[] pickups;
    private HitPoints hp;
    private WaveManager waveManager;
    private SpriteRenderer sr;

    private void Awake()
    {
        hp = GetComponent<HitPoints>();
        waveManager = GetComponentInParent<WaveManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        //Move right/left- when hitting the edge of screen, flip and go down 1 row
    }

    public void OnHit(int dmg)
    {
        hp.LoseHealth(dmg);
        if(hp.isDead) {
            waveManager.EnemyKilled();
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        if(Random.Range(0, 100) <= 33) {
            Debug.Log("Spawned a pickup!");
            //Instantiate(pickups[Random.Range(0, pickups.Length)]);
        }
        if(sr.color ==Color.black) {
            Debug.Log("allahu akbnar");
        }
    }
}
