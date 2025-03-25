using UnityEngine;

public class PlatformerBackground : MonoBehaviour
{
    [SerializeField] private Transform background1, background2;
    [SerializeField] private float scrollSpeed;

    
    private void FixedUpdate()
    {
        float scrollValue = Time.deltaTime * scrollSpeed;
        
        UpdateBackground(background1, scrollValue);
        UpdateBackground(background2, scrollValue);
    }

    private void UpdateBackground(Transform background, float scrollValue)
    {
        background.position += scrollValue * Vector3.right;
        
        float scale = background.localScale.x;
        
        if (background.position.x >= scale)
        {
            Vector3 newPosition = background.position;
            newPosition.x = newPosition.x % scale - scale;
            background.position = newPosition;
        }
    }
}
