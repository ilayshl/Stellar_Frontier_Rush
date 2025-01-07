using UnityEngine;

/// <summary>
/// Responsible for enemies in the game.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    private HitPoints hp;

    private void Awake()
    {
        hp = GetComponent<HitPoints>();
    }

    private void Update() {
        //Move right/left- when hitting the edge of screen, flip and go down 1 row
        //transform.position
    }

    public void OnHit(int dmg)
    {
        hp.LoseHealth(dmg);
    }
}
