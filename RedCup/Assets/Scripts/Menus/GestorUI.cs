using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GestorUI : MonoBehaviour
{
    [System.Serializable]
    public class PanelEntry
    {
        public PanelType type;
        public UIPanel panel;
    }

    [Header("Paneles registrados")]
    [SerializeField] private List<PanelEntry> paneles;

    private Dictionary<PanelType, UIPanel> panelDict;
    private UIPanel currentPanel;

    public static GestorUI Instance { get; private set; }

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        panelDict = new Dictionary<PanelType, UIPanel>();

        foreach (var entry in paneles)
        {
            if (entry.panel == null)
            {
                Debug.LogError("Panel no asignado: " + entry.type);
                continue;
            }
            panelDict.Add(entry.type, entry.panel);
            entry.panel.gameObject.SetActive(false);
        }
    }

    public void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Escena actual: " + sceneName);
    }
    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
                MostrarPanel(PanelType.Pausa);
            else
                MostrarPanel(PanelType.HUD);
        }
    }

    public void MostrarPanel(PanelType type)
    {
        foreach (var panel in panelDict.Values)
            panel.gameObject.SetActive(false);

        if (panelDict.ContainsKey(type))
            panelDict[type].gameObject.SetActive(true);
    }
    public void OcultarTodos()
    {
        foreach (var panel in panelDict.Values)
            panel.Ocultar();
    }
    public void Salir()
    { 
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public bool HasPanel(PanelType type)
    {
        return panelDict.ContainsKey(type);
    }
}