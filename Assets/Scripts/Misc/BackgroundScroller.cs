using UnityEngine;

/// <summary>
/// Responsible of the infinite scrolling backgrounds of the game.
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private float textureUnitSize;
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
    }
}
