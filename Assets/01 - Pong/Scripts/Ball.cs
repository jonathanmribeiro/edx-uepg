using UnityEngine;

namespace EDXUEPG.Pong
{
    public class Ball : MonoBehaviour
    {
        public Vector2 Direction;
        public float Speed;
        public bool HitLeftKillzone;
        public bool HitRightKillzone;

        /// <summary>
        /// Coloca a bola novamente no centro da tela e calcula uma nova direção para ela.
        /// </summary>
        public void Reposition()
        {
            transform.position = Vector3.zero;
            Direction = Random.insideUnitCircle + Vector2.one;
            HitLeftKillzone = false;
            HitRightKillzone = false;
        }

        /// <summary>
        /// Atualização do movimento da bola.
        /// </summary>
        public void UpdateBall()
        {
            transform.Translate(Speed * Time.deltaTime * Direction);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.gameObject.name)
            {
                case "LeftPaddle":
                    Direction = new Vector2(-Direction.x, Direction.y);
                    break;
                case "LeftKillzone":
                    HitLeftKillzone = true;
                    break;
                case "RightPaddle":
                    Direction = new Vector2(-Direction.x, Direction.y);
                    break;
                case "RightKillzone":
                    HitRightKillzone = true;
                    break;
                case "Top":
                    Direction = new Vector2(Direction.x, -Direction.y);
                    break;
                case "Bottom":
                    Direction = new Vector2(Direction.x, -Direction.y);
                    break;
            }
        }
    }
}