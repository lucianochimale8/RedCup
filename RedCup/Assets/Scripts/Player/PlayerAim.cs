using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("Aim")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float aimSpeed = 10f;
    [Header("Camera")]
    private Camera cam;
    [Header("Posicion del mouse")]
    private Vector2 mousePos;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // el aim sigue al player
        transform.position = playerTransform.position;

        // posicion del mouse en world space
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

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
