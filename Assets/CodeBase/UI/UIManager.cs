using System;
using System.Collections.Generic;
using CodeBase.Helpers;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI killCounterText;
        [SerializeField] private GameObject prefabUIBall;
        [SerializeField] private Transform parentCounterBalls;
        [SerializeField] private List<Transform> spawnPositionBalls;
        [SerializeField] private List<GameObject> ballsUI;


        private GameObject _currentBall;
        private int _kills;
        private int _availableBalls;

        private EventsHolder EventsHolder => EventsHolder.Instance;

        private void Start()
        {
            SetStartBallsUI();
        }

        private void OnEnable()
        {
            EventsHolder.killEvent.AddListener(UpdateKillsText);
            EventsHolder.spawnBall.AddListener(UpdateUIBalls);
            EventsHolder.destroyBall.AddListener(RemoveUIBalls);
        }

        private void OnDisable()
        {
            EventsHolder?.killEvent.RemoveListener(UpdateKillsText);
            EventsHolder?.spawnBall.RemoveListener(UpdateUIBalls);
            EventsHolder?.destroyBall.RemoveListener(RemoveUIBalls);
        }

        private void UpdateKillsText(GameObject cube)
        {
            _kills++;
            killCounterText.text = "Kills : " + _kills;
        }

        private void SetStartBallsUI()
        {
            _availableBalls = spawnPositionBalls.Count;
            foreach (var ball in spawnPositionBalls)
            {
                _currentBall = Instantiate(prefabUIBall, ball.position, Quaternion.identity, parentCounterBalls);
                ballsUI.Add(_currentBall);
            }
        }

        private void UpdateUIBalls()
        {
            if (_availableBalls >= 5) return;
            _currentBall = Instantiate(prefabUIBall, spawnPositionBalls[_availableBalls].position, Quaternion.identity,
                parentCounterBalls);
            ballsUI.Add(_currentBall);
            _availableBalls++;
        }

        private void RemoveUIBalls()
        {
            if (_availableBalls <= 0) return;
            _availableBalls--;
            Destroy(ballsUI[_availableBalls]);
            ballsUI.Remove(ballsUI[_availableBalls].gameObject);
        }
    }
}