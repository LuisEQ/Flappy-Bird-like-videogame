using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _startText,_scoreText, _gameOverText, _restartText;
    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: 00";
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Null game manager");
        }
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        _startText.gameObject.SetActive(false);
    }
    public void UpdateLives(int lives)
    {
        if (lives <= 0)
        {
            GameOverSequence();
        }
    }
    public void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";

    }
    private void GameOverSequence()
    {
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
        }
    }
}
