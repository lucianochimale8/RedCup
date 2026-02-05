using UnityEngine;

public class GestorUI : MonoBehaviour
{
    [Header("Colocar paneles en orden")]
    public UIPanel[] paneles;
    private UIPanel currentPanel;

    public void Start()
    {
        OcultarPaneles();
        MostrarPaneles(0);
    }

    public void MostrarPaneles(int index)
    { 
        if (index < 0 || index >= paneles.Length) 
        { 
            Debug.Log("error: incorrect index");
        } 
        if (currentPanel != null)
        {
            currentPanel.Ocultar();
        } 
        paneles[index].Mostrar();
        currentPanel = paneles[index];
    }
    public void OcultarPaneles()
    {
        for (int i = 0; i < paneles.Length; i++)
        {
            paneles[i].Ocultar();
        }
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