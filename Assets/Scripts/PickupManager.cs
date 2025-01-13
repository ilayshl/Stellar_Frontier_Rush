using UnityEngine;

public class PickupManager : MonoBehaviour
{
    int pickupType;
    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Debug.Log("check");
        pickupType = Random.Range(0, 3);
        switch (pickupType)
        {
            case 1:
                Debug.Log("pickup is health");
                break;
            case 2:
                Debug.Log("pickup is fire rate");
                break;
            case 3:
                Debug.Log("pickup is damage");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController pController))
        {
            switch (pickupType)
            {
                case 1:
                    // sr.color = Color.red;
                    break;
                case 2:
                    pController.IncreaseFireSpeed(0.1f);
                    break;
                case 3:
                    pController.IncreaseSpeed(1);
                    break;
            }
            Destroy(gameObject);
        }
    }

}
