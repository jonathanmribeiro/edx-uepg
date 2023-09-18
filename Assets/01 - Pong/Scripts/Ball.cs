using UnityEngine;

namespace edxuepg.pong
{
    public class Ball : MonoBehaviour
    {
        public Vector2 Direction;
        public float Speed;
        public bool HitLeftKillzone;
        public bool HitRightKillzone;

        public void Reposition()
        {
            transform.position = Vector3.zero;
            Direction = Random.insideUnitCircle + Vector2.one;
            HitLeftKillzone = false;
            HitRightKillzone = false;
        }

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