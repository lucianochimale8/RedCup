using UnityEngine;
using TMPro;

public class LevelCompletedUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro levelCompletedText;

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
    }
}
