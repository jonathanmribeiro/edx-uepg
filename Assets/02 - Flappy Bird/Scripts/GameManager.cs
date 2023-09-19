using UnityEngine;

namespace EDXUEPG.Flappy
{
    public class GameManager : MonoBehaviour
    {
        public IGameState CurrentGameState;

        private CountdownState _countdownState;
        private PlayState _playState;
        private ScoreState _scoreState;
        private TitleScreenState _titleScreenState;

        public GameObject TitleScreenUI;
        public GameObject ScoreUI;
        public GameObject PlayUI;
        public GameObject CountdownUI;

        public GameObject Bird;

        public int LastScore;
        public int HighestScore;

        public GameObject SinglePipePrefab;
        public GameObject DoublePipePrefab;
        public GameObject Background;
        public GameObject Ground;

        void Start()
        {
            _countdownState = new(this, CountdownUI);
            _playState = new(this, PlayUI, Bird, SinglePipePrefab, DoublePipePrefab, Background, Ground);
            _scoreState = new(this, ScoreUI);
            _titleScreenState = new(this, TitleScreenUI);

            Bird.GetComponent<BirdManager>().Prepare(_playState);

            EnterTitleScreenState();
        }

        void Update()
        {
            CurrentGameState.UpdateState();
        }

        public void EnterCountdownState()
        {
            SwitchState(_countdownState);
        }

        public void EnterPlayState()
        {
            SwitchState(_playState);
        }

        public void EnterScoreState()
        {
            SwitchState(_scoreState);
        }

        public void EnterTitleScreenState()
        {
            SwitchState(_titleScreenState);
        }

        private void SwitchState(IGameState newState)
        {
            TitleScreenUI.SetActive(false);
            ScoreUI.SetActive(false);
            PlayUI.SetActive(false);
            CountdownUI.SetActive(false);

            Bird.SetActive(false);

            CurrentGameState = newState;
            CurrentGameState.EnterState();
        }
    }
}