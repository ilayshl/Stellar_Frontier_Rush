using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    [SerializeField] private int initialHP;
    private int currentHP;

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
        if(currentHP<=0){
            Destroy(this);
        }
    }
}
