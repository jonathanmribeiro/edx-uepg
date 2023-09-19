using UnityEngine;

namespace EDXUEPG.Flappy
{
    public class TitleScreenState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameObject _titleScreenUI;

        public TitleScreenState(GameManager gameManager, GameObject titleScreenUI)
        {
            _gameManager = gameManager;
            _titleScreenUI = titleScreenUI;
        }

        public void EnterState()
        {
            _titleScreenUI.SetActive(true);
        }

        public void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _gameManager.EnterCountdownState();
            }
        }
    }
}