using UnityEngine;

/// <summary>
/// Health of an object.
/// </summary>
public class HitPoints
{
    private int _initialHP;
    private bool _isDead = false;
    private int _currentHP;


    public HitPoints(int initialHP = 10)
    {
        this._currentHP = initialHP;
        this._initialHP = initialHP;
    }

    /// <summary>
    /// Returns read-only Health int.
    /// </summary>
    /// <returns></returns>
    public int Health(){
        return _currentHP;
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
        _currentHP+=hpGained;
        _currentHP = Mathf.Min(_currentHP, _initialHP);
    }

    /// <summary>
    /// Decreases health and checks if it is equals or lower than 0.
    /// </summary>
    /// <param name="hpLost"></param>
    public void LoseHealth(int hpLost){
        _currentHP-=Mathf.Abs(hpLost);
        if(_currentHP<=0){
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
        _currentHP = value;
        }
        else
        {
            Debug.Log("Health can't be set to 0 or lower.");
        }
    }
}
