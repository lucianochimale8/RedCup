using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LevelCompletedUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro levelCompletedText;
    [SerializeField] private AudioClip nextLevelClip;
    [SerializeField] private float volumen;
    private void OnEnable()
    {
        GameEvents.OnLevelCompleted += ShowLevelCompleted;
    }
    private void OnDisable()
    {
        GameEvents.OnLevelCompleted -= ShowLevelCompleted;
    }

    private void ShowLevelCompleted()
    {
        levelCompletedText.gameObject.SetActive(true);
        AudioManager.Instance.PlaySoundEffect(nextLevelClip, volumen);
    }
}
