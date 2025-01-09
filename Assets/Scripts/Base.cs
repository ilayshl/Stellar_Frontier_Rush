using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsible for the player's base. With each enemy striking the base, hp will be lost, until reaching 0.
/// </summary>
public class Base: MonoBehaviour {
    private HitPoints hp;
    private SpriteRenderer sr;
    private const int initialHP = 10;

    private void Awake() {
        hp=GetComponent<HitPoints>();
        sr=GetComponent<SpriteRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent<Enemy>(out Enemy enemy)) {
            int dmg = 1;
            hp.LoseHealth(dmg);
            Debug.Log($"Base lost {dmg} health! \n Current health: {hp.Health()}");
            enemy.OnHit(100);
            if(hp.IsDead()) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if(hp.Health()==1) {
                if(sr.color!=Color.white) {
                    sr.color=Color.red;
                }
            }
        }
    }

}
