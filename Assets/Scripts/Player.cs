using UnityEngine;
public class Player : MonoBehaviour
{
    public float moveSpeed = 10;
    public float rotationSpeed = 30;
    public float jumpForce = 50;

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
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        Vector3 MoveVector = new(/*0*/ Input.GetAxis("Horizontal") /**/, 0, Input.GetAxis("Vertical"));
        MoveVector *= moveSpeed;
        rb.AddRelativeForce(MoveVector);
    }

    private void Rotate()
    {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
    }
}
