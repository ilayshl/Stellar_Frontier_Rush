using UnityEngine;

/// <summary>
/// Health of an object.
/// </summary>
public class HitPoints
{
    private int initialHP;
    private bool isDead = false;
    private int currentHP;


    public HitPoints(int initialHP = 5)
    {
        this.currentHP = initialHP;
    }

    /// <summary>
    /// Returns read-only Health int.
    /// </summary>
    /// <returns></returns>
    public int Health(){
        return currentHP;
    }
    
    /// <summary>
    /// Returns if Health equals or is lower than 0.
    /// </summary>
    /// <returns></returns>
    public bool IsDead() {
        return isDead;
    }

    /// <summary>
    /// Adds health, with a maximum of initialHP.
    /// </summary>
    /// <param name="hpGained"></param>
    public void GainHealth(int hpGained){
        currentHP+=hpGained;
        currentHP = Mathf.Min(currentHP, initialHP);
    }

    /// <summary>
    /// Decreases health and checks if it is equals or lower than 0.
    /// </summary>
    /// <param name="hpLost"></param>
    public void LoseHealth(int hpLost){
        currentHP-=Mathf.Abs(hpLost);
        if(currentHP<=0){
            isDead=true;
        }
    }

    /// <summary>
    /// Specifically sets current health to a value.
    /// </summary>
    /// <param name="value"></param>
    public void SetHealth(int value)
    {
        if(value>0)
        {
        currentHP = value;
        }
        else
        {
            Debug.Log("Health can't be set to 0 or lower.");
        }
    }
}
