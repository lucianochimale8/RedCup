using UnityEngine;
using System.Collections.Generic;

public class GestorUI : MonoBehaviour
{
    [System.Serializable]
    public class PanelEntry
    {
        public PanelType type;
        public UIPanel panel;
    }

    [Header("Colocar paneles en orden")]
    [SerializeField] private List<PanelEntry> paneles;

    private UIPanel currentPanel;

    private Dictionary<PanelType, UIPanel> panelDict;

    private void Awake()
    {
        panelDict = new Dictionary<PanelType, UIPanel>();

        foreach (var entry in paneles)
        {
            panelDict.Add(entry.type, entry.panel);
        }
    }

    public void Start()
    {
        Time.timeScale = 1f;
        OcultarPaneles();
        MostrarPaneles(PanelType.MenuInicio);
    }

    public void MostrarPaneles(PanelType type)
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

    public void OcultarPaneles()
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

        //Application.Quit();
    }
}
