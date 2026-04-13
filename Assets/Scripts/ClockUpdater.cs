using System.Collections;
using UnityEngine;

public class ClockUpdater : MonoBehaviour
{
    public float CountdownStartValue = 30;

    public Material material;
    float Countdown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Countdown = CountdownStartValue;
        material.SetFloat("_Countdown", Mathf.FloorToInt(Countdown));
        StartCoroutine(Clock());
    }

    private IEnumerator Clock()
    {
        if (Countdown > 0)
        {
            yield return new WaitForSeconds(1);
            Countdown--;
        }
        else
        {
            Countdown = CountdownStartValue;
        }
        material.SetFloat("_Countdown", Mathf.FloorToInt(Countdown));

        yield return null;

        StartCoroutine(Clock());
    }
}
