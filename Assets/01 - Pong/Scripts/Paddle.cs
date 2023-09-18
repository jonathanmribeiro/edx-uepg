using UnityEngine;

namespace edxuepg.pong
{
    public class Paddle : MonoBehaviour
    {
        public float Speed;

        private void Start()
        {
            transform.position = new Vector3(transform.position.x, 0);
        }

        public void UpdatePaddle(float inputDirection)
        {
            Vector2 direction = new Vector2(0, inputDirection);
            transform.Translate(direction * Time.deltaTime * Speed);
        }
    }
}