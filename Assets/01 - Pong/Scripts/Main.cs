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

        /// <summary>
        /// Método de inicialização do exemplo.
        /// </summary>
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

        /// <summary>
        /// Loop principal. Chama os métodos de atualização de acordo com o estado atual.
        /// </summary>
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

        /// <summary>
        /// Atualizador no estado de Start.
        /// </summary>
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

        /// <summary>
        /// Atualizador no estado de Serve.
        /// </summary>
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

        /// <summary>
        /// Atualizador no estado de Play.
        /// </summary>
        private void UpdatePlayState()
        {
            StartUI.SetActive(false);
            ServeUI.SetActive(false);
            DoneUI.SetActive(false);

            _rightPaddleDirection = ComputeDirectionFromInputs(KeyCode.UpArrow, KeyCode.DownArrow);
            
            RightPaddle.UpdatePaddle(_rightPaddleDirection);

            _leftPaddleDirection = ComputeDirectionFromInputs(KeyCode.W, KeyCode.S);
            
            LeftPaddle.UpdatePaddle(_leftPaddleDirection);

            Ball.UpdateBall();

            if (Ball.HitLeftKillzone)
            {
                RightScore++;
                GameState = GameStates.Done;
            }
            else if (Ball.HitRightKillzone)
            {
                LeftScore++;
                GameState = GameStates.Done;
            }
        }

        /// <summary>
        /// Atualizador no estado de Done.
        /// </summary>
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

        /// <summary>
        /// Usado no método de atualização do estado Play. Calcula uma direção (-1, 0, 1) de acordo com a entrada.
        /// </summary>
        /// <param name="upKey">[UpArrow, W]</param>
        /// <param name="downKey">[DownArrow, S]</param>
        /// <returns></returns>
        private float ComputeDirectionFromInputs(KeyCode upKey, KeyCode downKey)
        {
            bool keyUpPressed = Input.GetKey(upKey);
            bool keyDownPressed = Input.GetKey(downKey);

            if (keyUpPressed)
            {
                return 1;
            }
            else if (keyDownPressed)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}