using System.Collections;
using UnityEngine;

public class LightStripManager : MonoBehaviour
{
    [SerializeField] private Transform[] lightPositions;
    [SerializeField] private Transform[] lightStrips;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDelay;

    private int counter = 1;
    void Start()
    {
        for (int i = 0; i < lightStrips.Length; i++)
        {
            var v = i % lightPositions.Length;
            lightStrips[i].position = lightPositions[v].position;
        }

        StartCoroutine(MoveLight());
    }

    IEnumerator MoveLight()
    {
        for (int i = 0; i < lightStrips.Length; i++) 
        {
            var v = (i + counter) % lightPositions.Length;
            var dist = Mathf.Infinity;
            var startPos = lightStrips[i].position;

            while (dist > .2f)
            {
                yield return null;
                dist = (lightPositions[v].position - lightStrips[i].position).magnitude;
                Debug.Log($"the distance for moving from {lightPositions[v].position} to {lightStrips[i].position} = {dist}");
                lightStrips[i].position = Vector3.Lerp(startPos, lightPositions[v].position, Time.deltaTime * moveSpeed);
            }
            counter++;

            yield return new WaitForSeconds(moveDelay);
        }

        StartCoroutine(MoveLight());
    }
}
