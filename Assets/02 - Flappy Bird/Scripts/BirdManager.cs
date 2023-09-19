using UnityEngine;

namespace EDXUEPG.Flappy
{
    public class BirdManager : MonoBehaviour
    {
        private PlayState _playState;

        public void Prepare(PlayState playState)
        {
            _playState = playState;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _playState?.OnBirdCollision(collision.gameObject.name);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _playState?.OnBirdCollision(collision.gameObject.name);
        }
    }
}