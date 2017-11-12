using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour 
{
    public float moveSpeed;

    private Rigidbody2D rb;
    private Camera cam;
    private Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        // Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
        Vector2 mousePos = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        LookAt(mousePos);

        velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;

        Debug.Log(velocity);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void LookAt(Vector2 p2)
    {
        float forwardAngle = 0;
        Vector2 p1 = transform.position;
        float opp = p2.y - p1.y;
        float adj = p2.x - p1.x;
        float zRadians = Mathf.Atan((p2.y - p1.y) / (p2.x - p1.x));
        if (adj < 0)
            zRadians += Mathf.PI;
        Quaternion q = Quaternion.Euler(0, 0, Mathf.Rad2Deg * zRadians);
        transform.rotation = q;
        transform.Rotate(0, 0, -forwardAngle);
    }
}
