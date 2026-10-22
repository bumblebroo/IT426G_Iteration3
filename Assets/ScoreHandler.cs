using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private ScoreCounter scoreCounter;

    public UnityEvent OnScoreIncrease => scoreCounter.OnScoreIncrease;

    private void Start() {
        scoreCounter = new ScoreCounter();
        scoreText.text = scoreCounter.Score.ToString();
    }

    public void ScoreEvent(int score) {
        scoreCounter.AddScore(score);
        scoreText.text = scoreCounter.Score.ToString();
    }
}
