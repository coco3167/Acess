using UnityEngine;

public class PlatformerBackground : MonoBehaviour
{
    [SerializeField] private RectTransform background1, background2;
    [SerializeField] private float scrollSpeed;
    
    private float m_imageWidth;

    private void Awake()
    {
        Sprite sprite = background1.GetComponent<SpriteRenderer>().sprite;
        m_imageWidth = sprite.textureRect.width / sprite.pixelsPerUnit;
        background2.anchoredPosition = new Vector2(background1.position.x + m_imageWidth, background1.position.y);
    }

    private void FixedUpdate()
    {
        float scrollValue = Time.deltaTime * scrollSpeed;
        
        UpdateBackground(background1, scrollValue);
        UpdateBackground(background2, scrollValue);
    }

    private void UpdateBackground(RectTransform background, float scrollValue)
    {
        background.position -= scrollValue * Vector3.right;
        
        if (background.position.x <= -m_imageWidth) 
            background.Translate(2*m_imageWidth, 0, 0);
    }
}
