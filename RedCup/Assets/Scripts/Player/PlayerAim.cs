using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("Aim")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float aimSpeed = 10f;
    
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (playerTransform == null || cam == null) return;
        // posicion inicial del aim que seguira al jugador
        transform.position = playerTransform.position;

        // posicion del mouse en world space
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // direccion hacia el mouse
        Vector2 direction = mousePos - (Vector2)transform.position;

        // rotación suave
        transform.right = Vector2.MoveTowards(transform.right, direction, aimSpeed * Time.deltaTime);

        // flip visual
        if (transform.right.x < 0)
            transform.localScale = new Vector3(1, -1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
