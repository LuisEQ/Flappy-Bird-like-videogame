using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isgameOver;
    private bool _isgameStart;
    [SerializeField] private SpawnManagerPipes _spawnManager;
    [SerializeField] private HUD _hud;
    [SerializeField] private PlayerBehavior _player;

    // Start is called before the first frame update
    void Start()
    {
        _isgameOver = false;
        _isgameStart = false;

        _player.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !_isgameStart)
        {
            _spawnManager.OnPlayerStart();
            _isgameStart = true;
            _hud.StartGame();
            _player.gameObject.SetActive(true);
        }
        if (Input.anyKey && _isgameOver)
        {
            SceneManager.LoadScene(0); //Game scene (current game scene)
        }
    }
    public void GameOver()
    {
        _isgameOver = true;
    }
}
