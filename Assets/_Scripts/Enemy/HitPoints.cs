using UnityEngine;

/// <summary>
/// Health of an object.
/// </summary>
public class HitPoints
{
    public int initialHP {get; private set;}
    public int currentHP {get; private set;}
    private bool _isDead = false;


    public HitPoints(int initialHP = 10)
    {
        this.currentHP = initialHP;
        this.initialHP = initialHP;
    }
    
    /// <summary>
    /// Returns if Health equals or is lower than 0.
    /// </summary>
    /// <returns></returns>
    public bool IsDead() {
        return _isDead;
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
            _isDead=true;
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
