using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private string containerName = "hearts-container";

    private List<VisualElement> hearts = new List<VisualElement>();

    private void Awake()
    {
        if (uiDocument == null)
            uiDocument = GetComponent<UIDocument>();

        InicializarCorazones();
    }
    private void InicializarCorazones()
    {
        if (uiDocument == null || uiDocument.rootVisualElement == null) return;

        VisualElement container = uiDocument.rootVisualElement.Q<VisualElement>(containerName);

        if (container != null)
        {
            hearts.Clear();
            // Guarda todos los elementos hijos del contenedor
            foreach (var child in container.Children())
            {
                hearts.Add(child);
            }
        }
    }
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
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].style.display = (i < lives) ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
