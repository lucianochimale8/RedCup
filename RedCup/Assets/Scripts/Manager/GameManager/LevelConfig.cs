using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    [SerializeField] private bool startWithWand;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetWand(startWithWand);
        }
    }
}
