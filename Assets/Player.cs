using UnityEngine;
public class Player : MonoBehaviour
{
    public float MV_SPD = 10;
    public float Rot_SPD = 30;

    private Camera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 MoveVector = new(/*0*/ Input.GetAxis("Horizontal") /**/, 0, Input.GetAxis("Vertical"));
        MoveVector *= Time.deltaTime * MV_SPD;
        transform.position += MoveVector;
    }

    private void Rotate()
    {
        float rotationH = Input.GetAxis("Mouse X") * Rot_SPD * Time.deltaTime;
        float rotationV = Input.GetAxis("Mouse Y") * Rot_SPD * Time.deltaTime;
        camera.transform.Rotate(rotationV, rotationH, 0);
    }
}
