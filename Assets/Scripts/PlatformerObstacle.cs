using UnityEngine;

public class PlatformerObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
           other.GetComponent<PlatformerController>().HitObstacle();
    }
}
