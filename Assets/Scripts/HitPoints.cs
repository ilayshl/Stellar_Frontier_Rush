using UnityEngine;

/// <summary>
/// Health of an object.
/// </summary>
public class HitPoints : MonoBehaviour
{
    [SerializeField] private int initialHP;
    private bool isDead = false;
    private int currentHP;


    void Start() {
        currentHP=initialHP;
    }

    //Returns read-only Health int.
    public int Health(){
        return currentHP;
    }
    
    //Returns if isDead- in other words, if Health equals or is lower than 0.
    public bool IsDead() {
        return isDead;
    }

    //Adds health, with a maximum of initialHP.
    public void GainHealth(int hpGained){
        currentHP+=hpGained;
        Mathf.Min(currentHP, initialHP);
    }

    //Decreases health and checks if it is equals or lower than 0.
    public void LoseHealth(int hpLost){
        currentHP-=hpLost;
        if(currentHP<=0){
            isDead=true;
        }
    }
}
