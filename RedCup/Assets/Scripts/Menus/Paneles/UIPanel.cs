using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIPanel : MonoBehaviour
{
    [SerializeField] protected string containerName;
    protected VisualElement container;
    public virtual void Inicializar(VisualElement root)
    {
        if (root == null) return;
        container = root.Q<VisualElement>(containerName);
    }
    public virtual void Mostrar()
    {
        if (container != null)
            container.style.display = DisplayStyle.Flex;
    }

    public virtual void Ocultar()
    {
        if (container != null)
            container.style.display = DisplayStyle.None;
    }
}
