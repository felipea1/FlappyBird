using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player;
    private Spawner spawner;

    public Text scoreText;
    public Text bestText;
    public Text scoreTXT;
    public Text bestTXT;

    public GameObject playButton;
    public GameObject gameOver;
    public GameObject QuitButton;
    public GameObject Title;

    public AudioClip scoreIncreaseSound;
    public AudioClip dieSound;

    public int score { get; private set; }
    private int bestScore;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        Title.SetActive(true);
        playButton.SetActive(true);
        gameOver.SetActive(false);
        QuitButton.SetActive(true);
        scoreText.gameObject.SetActive(false);
        bestText.gameObject.SetActive(false);
        scoreTXT.gameObject.SetActive(false);
        bestTXT.gameObject.SetActive(false);

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestText.text = bestScore.ToString();

        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        QuitButton.SetActive(false);
        Title.SetActive(false);
        scoreText.gameObject.SetActive(true);
        bestText.gameObject.SetActive(true);
        scoreTXT.gameObject.SetActive(true);
        bestTXT.gameObject.SetActive(true);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);
        QuitButton.SetActive(true);
        Title.SetActive(false);
        scoreText.gameObject.SetActive(true);
        bestText.gameObject.SetActive(true);
        scoreTXT.gameObject.SetActive(true);
        bestTXT.gameObject.SetActive(true);

        if (dieSound != null)
        {
            AudioSource.PlayClipAtPoint(dieSound, Camera.main.transform.position);
        }

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

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

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            bestText.text = bestScore.ToString();
        }

        if (scoreIncreaseSound != null)
        {
            AudioSource.PlayClipAtPoint(scoreIncreaseSound, Camera.main.transform.position);
        }
    }
}