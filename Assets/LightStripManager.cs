using System.Collections;
using UnityEngine;

public class LightStripManager : MonoBehaviour
{
    [SerializeField] private Transform[] lightPositions;
    [SerializeField] private Transform[] lightStrips;
    [SerializeField] private Material mat;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDelay;

    private int counter = 1;
    private float CornerpassDegree = 0;
    private int[] stripIndex;
    void Start()
    {
        stripIndex = new int[lightStrips.Length];

        for (int i = 0; i < lightStrips.Length; i++)
        {
            var v = (stripIndex[i] + 1) % lightPositions.Length;
            lightStrips[i].position = lightPositions[v].position;
            lightStrips[i].rotation = Quaternion.Euler(90, 0, CornerpassDegree + 90 * i);
        }

        StartCoroutine(MoveLights());
    }

    IEnumerator MoveLights()
    {
        for (int i = 0; i < lightStrips.Length; i++) 
        {
            StartCoroutine(MoveLight(i));
        }

        yield return new WaitForSeconds(moveDelay);
        StartCoroutine(MoveLights());
    }

    IEnumerator MoveLight(int i)
    {
        var v = (stripIndex[i] + 1) % lightPositions.Length;

        var startPos = lightStrips[i].position;
        var targetPos = lightPositions[v].position;

        var dist = Vector3.Distance(startPos, targetPos);
        var duration = dist / moveSpeed;
        float passedTime = 0f;

        if (duration > 0)
        {
            while (passedTime < duration)
            {
                yield return null;
                passedTime += Time.deltaTime;

                var step = passedTime / duration;

                lightStrips[i].position = Vector3.Lerp(startPos, targetPos, step);
                dist = Vector3.Distance(lightStrips[i].position, targetPos);
                //Debug.Log($"the distance for moving from {lightPositions[v].position} to {lightStrips[i].position} = {dist}");
            }
        }

        lightStrips[i].position = targetPos;

        lightStrips[i].rotation = Quaternion.Euler(0, 0, 0);
        mat.SetFloat("_CornerStartTime", Time.time);
        mat.SetFloat("_Move", 1);
        yield return new WaitForSeconds(moveDelay);
        mat.SetFloat("_Move", 0);
        CornerpassDegree += 90;
        lightStrips[i].rotation = Quaternion.Euler(90, 0, CornerpassDegree + 90 * i);
        stripIndex[i] = v;
    }
}
