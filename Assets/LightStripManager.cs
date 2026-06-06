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
    private int[] stripIndex;
    void Start()
    {
        stripIndex = new int[lightStrips.Length];

        for (int i = 0; i < stripIndex.Length; i++) 
        { 
            stripIndex[i] = i;
        }

        for (int i = 0; i < lightStrips.Length; i++)
        {
            var v = (stripIndex[i] + 1) % lightPositions.Length;
            //Debug.Log($"light strip {lightStrips[i].name} has strip index {stripIndex[i]} it should spawn at the position {lightPositions[i].position}");
            lightStrips[i].position = lightPositions[v].position;
            //Debug.Log($"just set the pos of light strip {lightStrips[i].name} to {lightPositions[v].position}");
            lightStrips[i].rotation = Quaternion.Euler(90, 0, 90 * i);
        }

        StartCoroutine(MoveLights());
    }
    IEnumerator MoveLights()
    {
        while (true)
        {
            for (int i = 0; i < lightStrips.Length; i++)
            {
                StartCoroutine(MoveLight(i));
            }

            yield return new WaitForSeconds(moveDelay);
        }
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
            }
        }

        lightStrips[i].position = targetPos;

        //lightStrips[i].rotation = Quaternion.Euler(0, 0, 0);

        Debug.Log($"Corner reached by {i} at {Time.time}");

        yield return new WaitForSeconds(moveDelay);

        lightStrips[i].rotation = Quaternion.Euler(90, 0, 90 * v);
        stripIndex[i] = v;
    }
}
