using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ScoreCounter
{
    private int score;

    public UnityEvent OnScoreIncrease = new UnityEvent();

    public int Score => score;
    public ScoreCounter() {
        score = 0;
    }

    public ScoreCounter(int startingScore) {
        if(startingScore < 0) {
            score = 0;
            return;
        }
        score = startingScore;
    }

    public void AddScore(int scoreDelta) {
        if(scoreDelta < 0) {
            return;
        }
        score += scoreDelta;
        OnScoreIncrease?.Invoke();
    }
}
