using UnityEngine;

/// <summary>
/// Health
/// </summary>
public class HitPoints : MonoBehaviour
{
    [SerializeField] private int initialHP;
    private bool isDead = false;
    private int currentHP;


    void Start() {
        currentHP=initialHP;
    }

    public int Health(){
        return currentHP;
    }

    public bool IsDead() {
        return isDead;
    }

    public void GainHealth(int hpGained){
        currentHP+=hpGained;
        Mathf.Min(currentHP, initialHP);
    }

    public void LoseHealth(int hpLost){
        currentHP-=hpLost;
        if(currentHP<=0){
            isDead=true;
        }
    }
}
