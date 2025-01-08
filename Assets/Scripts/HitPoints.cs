using UnityEngine;

/// <summary>
/// Health
/// </summary>
public class HitPoints : MonoBehaviour
{
    public bool isDead = false;
    [SerializeField] private int initialHP;
    private int currentHP;

    public int Health(){
        return currentHP;
    }

    void Start()
    {
        currentHP=initialHP;
    }

    public void GainHealth(int hpGained){
        currentHP+=hpGained;
        Mathf.Min(currentHP, initialHP);
    }

    public void LoseHealth(int hpLost){
        currentHP-=hpLost;
        Debug.Log(currentHP);
        if(currentHP<=0){
            isDead=true;
        }
    }
}
