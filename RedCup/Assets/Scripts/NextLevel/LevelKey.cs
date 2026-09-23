using UnityEngine;

public class LevelKey : MonoBehaviour
{
    public enum KeyState
    {
        Idle,
        Collected
    }

    [SerializeField] private AudioClip keySound;
    [SerializeField] private float volume = 1f;

    private KeyState currentState = KeyState.Idle;
    public KeyState CurrentState => currentState;

    private Collider2D keyCollider;

    private void Awake()
    {
        keyCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState != KeyState.Idle)
            return;

        if (collision.CompareTag("Player"))
        {
            CollectKey();
        }
    }
    private void CollectKey()
    {
        currentState = KeyState.Collected;

        // Aislamiento físico inmediato
        if (keyCollider != null)
            keyCollider.enabled = false;

        // Emisión de eventos y efectos
        GameEvents.RaiseKeyCollected();

        if (AudioManager.Instance != null && keySound != null)
        {
            AudioManager.Instance.PlaySoundEffect(keySound, volume);
        }

        Destroy(gameObject);
    }
}
