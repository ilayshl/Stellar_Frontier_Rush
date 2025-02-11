using UnityEngine;

/// <summary>
/// Moves the UI assigned according to the mouse location on the screen.
/// </summary>
public class UIMovement : MonoBehaviour
{
    [SerializeField] private Vector2 movementMultiplier;
    private Vector2 screenMiddle;
    private RectTransform rectTransform;

private void Awake() {
    rectTransform = GetComponent<RectTransform>();
}
    void Start()
    {
    screenMiddle = rectTransform.anchoredPosition;
    }

    private void LateUpdate() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 deltaMovement = screenMiddle-mousePosition;
        rectTransform.transform.position=new Vector3(deltaMovement.x*movementMultiplier.x, deltaMovement.y*movementMultiplier.y, 0);
    }
}
