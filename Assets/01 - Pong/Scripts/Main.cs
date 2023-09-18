using TMPro;
using UnityEngine;

namespace edxuepg.pong
{
    public class Main : MonoBehaviour
    {
        public GameStates GameState;

        public Paddle LeftPaddle;
        public Paddle RightPaddle;

        public Ball Ball;

        private float _rightPaddleDirection = 0;
        private float _leftPaddleDirection = 0;

        public GameObject StartUI;
        public GameObject ServeUI;
        public GameObject PlayUI;
        public GameObject DoneUI;

        public TMP_Text LeftScoreUI;
        public float LeftScore;
        public TMP_Text RightScoreUI;
        public float RightScore;

        private void Start()
        {
            LeftPaddle.Speed = 3;
            RightPaddle.Speed = 3;
            Ball.Speed = 3;
            LeftScore = 0;
            RightScore = 0;

            GameState = GameStates.Start;
            PlayUI.SetActive(true);
        }

        private void Update()
        {
            if (GameState == GameStates.Start)
            {
                UpdateStartState();
            }
            else if (GameState == GameStates.Serve)
            {
                UpdateServeState();
            }
            else if (GameState == GameStates.Play)
            {
                UpdatePlayState();
            }
            else if (GameState == GameStates.Done)
            {
                UpdateDoneState();
            }

            LeftScoreUI.text = LeftScore.ToString();
            RightScoreUI.text = RightScore.ToString();
        }

        private void UpdateStartState()
        {
            StartUI.SetActive(true);
            ServeUI.SetActive(false);
            DoneUI.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameState = GameStates.Serve;
                return;
            }
        }

        private void UpdateServeState()
        {
            StartUI.SetActive(false);
            ServeUI.SetActive(true);
            DoneUI.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameState = GameStates.Play;
                return;
            }

            Ball.Reposition();
        }

        private void UpdatePlayState()
        {
            StartUI.SetActive(false);
            ServeUI.SetActive(false);
            DoneUI.SetActive(false);

            bool keyUpPressed = Input.GetKey(KeyCode.UpArrow);
            bool keyDownPressed = Input.GetKey(KeyCode.DownArrow);

            if (keyUpPressed)
            {
                _rightPaddleDirection = 1;
            }
            else if (keyDownPressed)
            {
                _rightPaddleDirection = -1;
            }
            else
            {
                _rightPaddleDirection = 0;
            }

            RightPaddle.UpdatePaddle(_rightPaddleDirection);

            bool keyWPressed = Input.GetKey(KeyCode.W);
            bool keySPressed = Input.GetKey(KeyCode.S);

            if (keyWPressed)
            {
                _leftPaddleDirection = 1;
            }
            else if (keySPressed)
            {
                _leftPaddleDirection = -1;
            }
            else
            {
                _leftPaddleDirection = 0;
            }

            LeftPaddle.UpdatePaddle(_leftPaddleDirection);

            Ball.UpdateBall();

            if (Ball.HitLeftKillzone)
            {
                RightScore = RightScore + 1;
                GameState = GameStates.Done;
            }
            else if (Ball.HitRightKillzone)
            {
                LeftScore = LeftScore + 1;
                GameState = GameStates.Done;
            }
        }

        private void UpdateDoneState()
        {
            StartUI.SetActive(false);
            ServeUI.SetActive(false);
            DoneUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameState = GameStates.Serve;
                return;
            }
        }
    }
}