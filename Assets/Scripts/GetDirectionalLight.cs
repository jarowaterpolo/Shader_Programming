using Unity.Mathematics;
using UnityEngine;

public class GetDirectionalLight : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material mat;
    [SerializeField] private Vector3 lightVector;

    private void Start()
    {
        if (mat == null)
        {
            mat = GetComponent<Material>();
        }
    }


    private void Update()
    {
        lightVector = directionalLight.transform.forward;

        Debug.Log($"L . {lightVector.normalized}");
        Debug.DrawRay(directionalLight.transform.position, lightVector.normalized * 5, Color.red);

        setMaterialLightVector(lightVector.normalized);
        setMaterialLightColor();
    }

    void setMaterialLightVector(Vector3 LVector)
    {
        mat.SetVector("_LightDir", LVector);
    }

    void setMaterialLightColor()
    {
        mat.SetColor("_LightColor", directionalLight.color);
    }
}
