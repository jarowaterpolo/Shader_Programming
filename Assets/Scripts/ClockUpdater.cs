using System.Collections;
using UnityEngine;

public class ClockUpdater : MonoBehaviour
{
    [SerializeField] private float CountdownStartValue = 30;

    [SerializeField] private Material material;
    float Countdown;
    private void OnValidate()
    {
        if (material == null) return;
        material.SetFloat("_Countdown", 0);
    }
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
