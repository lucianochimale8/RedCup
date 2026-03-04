using UnityEngine;

public class LevelKey : MonoBehaviour
{
    private bool collected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.CompareTag("Player"))
        {
            collected = true;
            GameEvents.RaiseKeyCollected();
            Destroy(gameObject);
        }
    }
}
