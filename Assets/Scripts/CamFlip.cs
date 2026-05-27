using UnityEngine;

public class CamFlip : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var cam = GetComponent<Camera>();
        cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(new Vector3(-1,1,1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
