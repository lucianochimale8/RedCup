using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        panelDict = new Dictionary<PanelType, UIPanel>();

        foreach (var entry in paneles)
        {
            if (!panelDict.ContainsKey(entry.type))
                panelDict.Add(entry.type, entry.panel);
        }
    }

    public void Start()
    {
        Time.timeScale = 1f;
        OcultarTodos();

        // SOLO para la escena de menú
        if (panelDict.ContainsKey(PanelType.MenuInicio))
        {
            MostrarPanel(MenuStartup.panelInicial);
        }

        //MenuStartup.panelInicial = PanelType.MenuInicio;
    }
    private void Update()
    {
        // SOLO para escenas con pausa
        if (!panelDict.ContainsKey(PanelType.Pausa)) return;

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
        if (!panelDict.ContainsKey(type))
        {
            Debug.LogError("Panel no registrado: " + type);
            return;
        }

        if (currentPanel != null)
            currentPanel.Ocultar();

        currentPanel = panelDict[type];
        currentPanel.Mostrar();
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
}