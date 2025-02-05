using UnityEngine;

/// <summary>
/// Oscillates up and down an object by the oscillationStrength.
/// </summary>
public class OscillationManager : MonoBehaviour
{
    [SerializeField] private float oscillationSpeed = 1;
    [SerializeField] private float oscillationMagnitute = 1;
    [SerializeField] private float offset;

    private void Start() {
        oscillationSpeed = Random.Range(oscillationSpeed-offset, oscillationSpeed+offset);
        oscillationMagnitute = Random.Range(oscillationMagnitute-offset, oscillationMagnitute+offset);
    }

    void LateUpdate()
    {
        float currentX = transform.position.x;
        float currentY = transform.position.y;
        float newY = Mathf.Sin(Time.time * oscillationSpeed) * oscillationMagnitute * Time.deltaTime + currentY;
        transform.position = new Vector3(currentX, newY, 0);
    }
}
