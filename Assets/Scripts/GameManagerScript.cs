using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;

    private int _score = 0;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void exit()
    {
        Application.Quit();
        Debug.Log("EXIT");
    }
    public void AddScore(int amount)
    {
        _score += amount;
        _scoreText.text = "Score: " + _score;
    }
}
