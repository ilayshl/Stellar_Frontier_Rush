using UnityEngine;

/// <summary>
/// Responsible for the cursor.
/// </summary>
public class PointerCrosshair : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20;
    [SerializeField] SpriteRenderer cursorPointer;
    [SerializeField] SpriteRenderer cursorSpinner;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        GameManager.Instance.OnGameStateChanged += SetVisible;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= SetVisible;
    }

    void Update()
    {
        CheckForClick();
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorSpinner.transform.eulerAngles += Vector3.forward * rotationSpeed * Time.unscaledDeltaTime;
        cursorPointer.transform.eulerAngles -= Vector3.forward * rotationSpeed * Time.unscaledDeltaTime / 2;
    }

    private void SetVisible(GameState state)
    {
        bool isActive = state != GameState.Active;
        cursorSpinner.gameObject.SetActive(isActive);
        cursorPointer.gameObject.SetActive(isActive);
    }

    private void CheckForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cursorPointer.transform.localScale /= 1.2f;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cursorPointer.transform.localScale *= 1.2f;
        }
    }
}
