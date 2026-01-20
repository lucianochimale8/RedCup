using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbar;

    public void UpdateHealthBar(int maxHealth, int health)
    {
        healthbar.fillAmount = (float)health / maxHealth;
    }
}
