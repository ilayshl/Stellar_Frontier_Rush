
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private TextMeshPro bulletDamageText;
    [SerializeField] private TextMeshPro fireRateText;
    [SerializeField] private TextMeshPro movementSpeedText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetHealth(int value) {
        healthText.text=value.ToString();
    }

    public void SetDamage(int value) {
        bulletDamageText.text = value.ToString();
    }

    public void SetFireRate(int value) {
        fireRateText.text=value.ToString();
    }

    public void SetMovementSpeed(int value) {
        movementSpeedText.text=value.ToString();
    }

}
