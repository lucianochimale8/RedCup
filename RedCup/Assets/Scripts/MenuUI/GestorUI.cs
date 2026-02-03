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
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // si no hay panel o no está en pausa ? pausar
            if (Time.timeScale == 1)
                MostrarPaneles(1); // MenuPausa
            else
                MostrarPaneles(0); // volver al juego
        }
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
