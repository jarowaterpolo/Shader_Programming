using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] private Material material;

    [SerializeField] private int leftScore = 0;
    [SerializeField] private int rightScore = 0;

    private void OnValidate()
    {
        if (material == null) return;
        material.SetFloat("_LeftTeamScore", 0);
        material.SetFloat("_RightTeamScore", 0);
    }
    void Start()
    {
        SetScoreBoard();
    }
    public void IncreaseLeftScore()
    {
        leftScore++;
        ScoreCheckAbove99();
        SetScoreBoard();
    }
    public void IncreaseRightScore() 
    { 
        rightScore++;
        ScoreCheckAbove99();
        SetScoreBoard();
    }

    private void ScoreCheckAbove99()
    {
        if (leftScore > 99)
        {
            leftScore = 0;
        }

        if (rightScore > 99)
        {
            rightScore = 0;
        }
    }

    private void SetScoreBoard()
    {
        material.SetFloat("_LeftTeamScore", leftScore);
        material.SetFloat("_RightTeamScore", rightScore);
    }
}
