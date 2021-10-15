using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private Text _scoreText;

    [SerializeField] 
    private Text _gameOverText;

    [SerializeField] 
    private Text _restartText;

    private Player _player;
    [SerializeField] 
    private Image _livesImage;

    [SerializeField]
    private Sprite[] _liveSprites;

    // Start is called before the first frame update
    void Start()
    {
        _restartText.enabled = false;
        _gameOverText.enabled = false;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GetHealth() < 1)
        {
            _restartText.enabled = true;
            _gameOverText.enabled = true;
            StartCoroutine(GameOverFlickerRoutine());
            if (Input.GetButton("Restart"))
            {
                SceneManager.LoadScene("Game");
            }

        }
        
        _livesImage.sprite = _liveSprites[_player.GetHealth()];
        _scoreText.text = "Score: " + _player.GetScore();

        
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
