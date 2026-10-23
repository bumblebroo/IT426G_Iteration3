using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private PlayerCommonData playerCommonData;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void Start() {
        scoreText.text = playerCommonData.Score.ToString();
        playerCommonData.OnScoreIncrease.AddListener(UpdateScore);
    }

    private void OnDestroy() {
        playerCommonData.OnScoreIncrease.RemoveListener(UpdateScore);
    }

    private void UpdateScore() {
        scoreText.text = playerCommonData.Score.ToString();
    }
}
