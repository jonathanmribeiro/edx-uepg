using TMPro;
using UnityEngine;

namespace EDXUEPG.Flappy
{
    public class ScoreState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameObject _scoreUI;
        private readonly TMP_Text _scoreText;

        public ScoreState(GameManager gameManager, GameObject scoreUI)
        {
            _gameManager = gameManager;
            _scoreUI = scoreUI;

            for (int i = 0; i < _scoreUI.transform.childCount; i++)
            {
                Transform child = _scoreUI.transform.GetChild(i);

                if (child.gameObject.name.ToLower().Equals("scoremessage"))
                {
                    _scoreText = child.GetComponent<TMP_Text>();
                }
            }
        }

        public void EnterState()
        {
            _scoreUI.SetActive(true);

            if (_gameManager.LastScore > _gameManager.HighestScore)
            {
                _gameManager.HighestScore = _gameManager.LastScore;
            }
        }

        public void UpdateState()
        {
            _scoreText.text = $"Your score: {_gameManager.LastScore} Highest score: {_gameManager.HighestScore}";

            if (Input.GetKeyDown(KeyCode.Return))
            {
                _gameManager.EnterCountdownState();
            }
        }
    }
}