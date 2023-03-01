using CodeBase.Helpers;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Balls
{
    public class SpawnerBall : MonoBehaviour
    {
        [SerializeField] private GameObject prefabBall;
        [SerializeField] private int maxBalls;
        [SerializeField] private int cooldownSpawnBall;

        private Camera _camera;
        private GameObject _currentBall;
        private float _elapsedTime;
        private int _availableBalls;

        private EventsHolder EventsHolder => EventsHolder.Instance;

        private void Start()
        {
            _camera = Camera.main;
            _availableBalls = maxBalls;
        }

        private void Update()
        {
            AddAvailableBall();

            if (Input.GetMouseButtonDown(0)) 
                ShotBall();
        }

        private void AddAvailableBall()
        {
            if (_availableBalls < maxBalls)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= cooldownSpawnBall)
                {
                    EventsHolder.spawnBall.Invoke();
                    _availableBalls++;
                    _elapsedTime = 0;
                }
            }
        }

        private void InstantiateBall()
        {
            if (_availableBalls > 0)
                _currentBall = Instantiate(prefabBall, _camera.transform.position, Quaternion.identity);
        }

        private void ShotBall()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag(Constants.Plane))
                {
                    InstantiateBall();
                    if (_currentBall != null && _availableBalls > 0)
                    {
                        _availableBalls--;
                        _currentBall.transform.DOMove(hit.point, 1.5f).SetEase(Ease.Linear);
                        EventsHolder.destroyBall.Invoke();
                    }
                    else
                        Debug.Log("No Balls");
                }
            }
        }
    }
}