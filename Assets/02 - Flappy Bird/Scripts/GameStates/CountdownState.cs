using TMPro;
using UnityEngine;

namespace EDXUEPG.Flappy
{
    public class CountdownState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameObject _countdownUI;
        private readonly TMP_Text _countdownText;

        private int _currentCount;

        private float _initialTime;

        public CountdownState(GameManager gameManager, GameObject countdownUI)
        {
            _gameManager = gameManager;
            _countdownUI = countdownUI;

            _countdownText = _countdownUI.GetComponentInChildren<TMP_Text>();
        }

        public void EnterState()
        {
            _countdownUI.SetActive(true);
            _currentCount = 3;

            _initialTime = Time.time;
        }

        public void UpdateState()
        {
            if (Time.time - _initialTime > 1.0f && _currentCount >= 0)
            {
                if (_currentCount == 0)
                {
                    _gameManager.EnterPlayState();
                }

                _currentCount--;
                _initialTime = Time.time;
            }


            if (_currentCount > 0)
            {
                _countdownText.text = $"{_currentCount}";
            }
            else
            {
                _countdownText.text = "GO!!!";
            }
        }
    }
}