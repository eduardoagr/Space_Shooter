using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager: MonoBehaviour {
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartGame;
    [SerializeField]
    private Image _livesDisplay;
    [SerializeField]
    private List<Sprite> _sprites;

    private GameManager _gameManager;
    const string GAMEMANAGER = "Game_Manager";

    void Awake() {

        _scoreText.text = $"Score: {0}";
        _gameManager = GameObject.Find(GAMEMANAGER).GetComponent<GameManager>() ?? null;
    }

    internal void UpdateText(int score) {

        _scoreText.text = $"Score: {score}";
        _gameOverText.gameObject.SetActive(false);
    }

    internal void UpdateLives(int currentLives) {

        _livesDisplay.sprite = _sprites[currentLives];

        if (currentLives == 0) {
            GameOverSequence();
        }
    }
    private void GameOverSequence() {

        _gameManager.GameOver(true);
        _restartGame.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect() {

        while (true) {
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
