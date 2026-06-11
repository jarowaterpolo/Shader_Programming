using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class StartTimeline : MonoBehaviour
{
    [SerializeField] private float timeScale = 1;
    private PlayableDirector playableDirector;
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();

        playableDirector.RebuildGraph();
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(timeScale);
        playableDirector.Play();
    }
}
