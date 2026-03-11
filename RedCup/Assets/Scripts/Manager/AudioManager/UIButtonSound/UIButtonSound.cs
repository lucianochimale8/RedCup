using UnityEngine;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private float volume = 1f;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        AudioManager.Instance.PlaySoundEffect(clickSound, volume);
    }
}
