using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Player player;
    private Spawner spawner;

    public Text scoreText;
    public Text bestScoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public int score { get; private set; }

    private void Awake()
    {
        GameManager.instance = this;

        Application.targetFrameRate = 60;

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        spawner.ResetState();
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScore = score;
        }
        bestScoreText.text = "Best Score: " + bestScore.ToString();

        Pause();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

}