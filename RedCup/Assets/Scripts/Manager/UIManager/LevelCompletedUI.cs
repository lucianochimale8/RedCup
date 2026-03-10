using UnityEngine;
using TMPro;

public class LevelCompletedUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro levelCompletedText;
    [SerializeField] private AudioClip nextLevelClip;

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
        AudioManager.Instance.PlaySoundEffect(nextLevelClip, 0.5f);
    }
}
