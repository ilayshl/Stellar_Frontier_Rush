using UnityEngine;

/// <summary>
/// Responsible of the infinite scrolling backgrounds of the game.
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private bool isAffectedByTime;
    [SerializeField] private int secondsPerIncrement;
    private float textureUnitSize;
    private float timePassed;

    private const float SPEEDMULTIPLY = 1.05f;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSize = texture.height / sprite.pixelsPerUnit;
    }

    //Moves the object down endlessly by moving it upwards.
    void Update()
    {
        transform.position -= new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        if (Mathf.Abs(transform.position.y) >= -textureUnitSize)
        {
            float offsetY = transform.position.y % textureUnitSize;
            transform.position = new Vector3(transform.position.x, offsetY, 0);
        }

        if (isAffectedByTime)
        {
            timePassed += Time.deltaTime;
            if ((int)timePassed % secondsPerIncrement == 0 && (int)timePassed != 0)
            {
                timePassed = 0;
                scrollSpeed *= SPEEDMULTIPLY;
            }
        }
    }
}
