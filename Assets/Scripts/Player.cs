using UnityEngine;
public class Player : MonoBehaviour
{
    public float MV_SPD = 10;
    public float Rot_SPD = 30;
    public float JumpForce = 50;

    private float xRotation = 0;

    private Camera camera;
    private Rigidbody rb;

    private bool canJump;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.2f) && Input.GetKey(KeyCode.Space))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (canJump)
        {
            rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        Vector3 MoveVector = new(/*0*/ Input.GetAxis("Horizontal") /**/, 0, Input.GetAxis("Vertical"));
        MoveVector *= MV_SPD;
        rb.AddRelativeForce(MoveVector);
    }

    private void Rotate()
    {
            float mouseX = Input.GetAxis("Mouse X") * Rot_SPD * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * Rot_SPD * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
    }
}
