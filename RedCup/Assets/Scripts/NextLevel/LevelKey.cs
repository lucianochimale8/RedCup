using UnityEngine;

public class LevelKey : MonoBehaviour
{
    private bool collected;
    [SerializeField] private AudioClip keySound;
    [SerializeField] private float volume = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.CompareTag("Player"))
        {
            collected = true;
            GameEvents.RaiseKeyCollected();
            AudioManager.Instance.PlaySoundEffect(keySound,volume);
            Destroy(gameObject);
        }
    }
}
