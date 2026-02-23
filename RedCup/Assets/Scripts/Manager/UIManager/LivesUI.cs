using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private TMP_Text livesText;
    private void OnEnable()
    {
        GameEvents.OnLivesChanged += UpdateLives;
    }
    private void OnDisable()
    {
        GameEvents.OnLivesChanged -= UpdateLives;
    }
    private void Start()
    {
        if (GameManager.Instance != null)
            UpdateLives(GameManager.Instance.Lives);
    }
    private void UpdateLives(int lives)
    {
        livesText.text = "Lifes: " + lives;
    }

}
