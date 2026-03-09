using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts;
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
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < lives)
                hearts[i].SetActive(true);
            else
                hearts[i].SetActive(false);
        }
    }
}
