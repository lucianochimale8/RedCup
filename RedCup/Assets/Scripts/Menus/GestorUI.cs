using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GestorUI : MonoBehaviour
{
    public static GestorUI Instance { get; private set; }
    public PanelType PanelActual { get; private set; }

    public static PanelType PanelMenuAlCargar = PanelType.MenuInicio;

    [System.Serializable]
    public class PanelEntry
    {
        public PanelType type;
        public UIPanel panel;
    }

    [Header("Paneles registrados")]
    [SerializeField] private List<PanelEntry> paneles;

    private Dictionary<PanelType, UIPanel> panelDict;

    public static string MENU_SCENE = "MenuUI";
    public static string MENU_WIN = "Win";
    
    #region Unity Lifecycle
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
            panelDict[entry.type] = entry.panel;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GameEvents.OnPlayerDied += HandlePlayerDied;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameEvents.OnPlayerDied -= HandlePlayerDied;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var panel in panelDict.Values)
            panel.Ocultar();

        if (scene.name == MENU_SCENE)
        {
            MostrarPanel(PanelMenuAlCargar);
        }
        if (scene.name == MENU_WIN)
        {
            MostrarPanel(PanelType.Win);
        }
        
    }
    public void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Escena actual: " + sceneName);
    }
    #endregion
    public void MostrarPanel(PanelType type)
    {
        if (!panelDict.ContainsKey(type))
        {
            Debug.LogWarning("Panel no registrado: " + type);
            return;
        }

        foreach (var panel in panelDict.Values)
            panel.Ocultar();

        panelDict[type].Mostrar();

        PanelActual = type;

        Debug.Log("Panel actual: " + PanelActual);
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
    private void HandlePlayerDied()
    {
        MostrarPanel(PanelType.GameOver);
    }
}