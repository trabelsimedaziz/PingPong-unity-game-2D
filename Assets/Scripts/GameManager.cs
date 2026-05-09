using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scores")]
    public int playerScore = 0;
    public int computerScore = 0;
    public int scoreLimit = 6;

    [Header("UI Panels")]
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;

    [Header("UI Text")]
    public TMP_Text playerScoreText;
    public TMP_Text computerScoreText;
    public TMP_Text gameOverText;
    public TMP_Text winnerText;

    [Header("Game Objects")]
    public GameObject ball;
    public GameObject playerPaddle;
    public GameObject computerPaddle;

    private bool isPaused = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        UpdateScoreUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    // ---- PAUSE ----
    public void PauseGame()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // ---- SCORING ----
    public void PlayerScores()
    {
        playerScore+=4;
        UpdateScoreUI();
        if (playerScore >= scoreLimit)
            ShowGameOver("Player Wins!");
        else
            StartCoroutine(ResetBallAfterDelay());
    }

    public void ComputerScores()
    {
        computerScore+=4;
        UpdateScoreUI();
        if (computerScore >= scoreLimit)
            ShowGameOver("Computer Wins!");
        else
            StartCoroutine(ResetBallAfterDelay());
    }

    System.Collections.IEnumerator ResetBallAfterDelay()
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        ball.transform.position = Vector3.zero;

        yield return new WaitForSeconds(1f);

        ball.GetComponent<Ball>().AddStartingForce();
    }

    void UpdateScoreUI()
    {
        playerScoreText.text = playerScore.ToString();
        computerScoreText.text = computerScore.ToString();
    }

    // ---- GAME OVER ----
    void ShowGameOver(string winner)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        ball.transform.position = Vector3.zero;
        ball.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverText.text = "GAME OVER";
        winnerText.text = winner;
        Time.timeScale = 0f;
    }

    public void TriggerGameOver(string winner)
    {
        ShowGameOver(winner);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}