using UnityEngine;

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

    // Update is called once per frame
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
