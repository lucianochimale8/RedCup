using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entraste en rango de la Exit Door");

        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.TryExitLevel();
        }
    }
}
