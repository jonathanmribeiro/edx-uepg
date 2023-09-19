using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EDXUEPG.Flappy
{
    public class PlayState : IGameState
    {
        private readonly GameManager _gameManager;
        private readonly GameObject _playUI;
        private readonly GameObject _bird;
        private readonly GameObject _singlePipePrefab;
        private readonly GameObject _doublePipePrefab;
        private readonly GameObject _background;
        private readonly GameObject _ground;
        private readonly TMP_Text _scoreText;

        private readonly List<GameObject> _instantiatedPipes;

        private int _currentScore;
        private float _spawnTime;

        public PlayState(GameManager gameManager, GameObject playUI, GameObject bird, GameObject singlePipePrefab, GameObject doublePipePrefab, GameObject background, GameObject ground)
        {
            _gameManager = gameManager;
            _playUI = playUI;
            _bird = bird;
            _singlePipePrefab = singlePipePrefab;
            _doublePipePrefab = doublePipePrefab;
            _background = background;
            _ground = ground;
            _scoreText = _playUI.GetComponentInChildren<TMP_Text>();

            _spawnTime = Time.time;
            _instantiatedPipes = new();
        }


        public void EnterState()
        {
            _currentScore = 0;

            _bird.transform.position = new Vector2(_bird.transform.position.x, 0);

            CleanPipes();

            _playUI.SetActive(true);
            _bird.SetActive(true);
        }

        public void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _bird.transform.GetComponent<Rigidbody2D>().MovePosition(_bird.transform.position + Vector3.up);
                _bird.transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            _scoreText.text = $"Score: {_currentScore}";

            if (Time.time - _spawnTime > Random.Range(2.0f, 4.0f))
            {
                SpawnPipe();
                _spawnTime = Time.time;
            }

            MovePipes();
            OffsetElementMaterial(_background, 0.01f);
            OffsetElementMaterial(_ground, 0.05f);
        }

        public void OnBirdCollision(string colliderName)
        {
            if (colliderName.ToLower().Contains("pipe"))
            {
                FinishLevel();
            }
            else if (colliderName.ToLower().Contains("trigger"))
            {
                _currentScore++;
            }
            else if (colliderName.Equals("Killzone"))
            {
                FinishLevel();
            }
        }

        private void SpawnPipe()
        {
            GameObject prefabToInstantiate;
            Vector3 spawnPosition;

            if (Random.Range(0.0f, 1.0f) > 0.5f)
            {
                prefabToInstantiate = _singlePipePrefab;
                spawnPosition = new Vector3(15, Random.Range(-9.7f, -4.7f));
            }
            else
            {
                prefabToInstantiate = _doublePipePrefab;
                spawnPosition = new Vector3(15, Random.Range(-10.3f, -8.1f));
            }

            GameObject instantiatedPrefab = Object.Instantiate(prefabToInstantiate, spawnPosition, new Quaternion());

            _instantiatedPipes.Add(instantiatedPrefab);
        }

        private void MovePipes()
        {
            GameObject pipeToDestroy = null;
            foreach (GameObject pipe in _instantiatedPipes)
            {
                pipe.transform.Translate(3 * Time.deltaTime * Vector3.left);

                if (pipe.transform.position.x <= -15)
                {
                    pipeToDestroy = pipe;
                }
            }

            if (pipeToDestroy)
            {
                _instantiatedPipes.Remove(pipeToDestroy);
                Object.Destroy(pipeToDestroy);
            }
        }

        private void OffsetElementMaterial(GameObject element, float speed)
        {
            Vector2 textureOffset = element.GetComponent<SpriteRenderer>().material.mainTextureOffset;
            element.GetComponent<SpriteRenderer>().material.mainTextureOffset = textureOffset + speed * Time.deltaTime * Vector2.right;
        }

        private void FinishLevel()
        {
            _gameManager.LastScore = _currentScore;
            _gameManager.EnterScoreState();
        }

        private void CleanPipes()
        {
            foreach (GameObject pipe in _instantiatedPipes)
            {
                Object.Destroy(pipe);
            }

            _instantiatedPipes.Clear();
        }
    }
}