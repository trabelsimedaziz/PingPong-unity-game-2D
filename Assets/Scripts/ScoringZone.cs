using UnityEngine;

public class ScoringZone : MonoBehaviour
{
    public bool playerScores = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) return;

        if (playerScores)
            GameManager.Instance.PlayerScores();
        else
            GameManager.Instance.ComputerScores();
    }
}