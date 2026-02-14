using UnityEngine;

public class LevelKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        
        GameEvents.OnKeyCollected?.Invoke();
        Destroy(gameObject);
        
    }
}
