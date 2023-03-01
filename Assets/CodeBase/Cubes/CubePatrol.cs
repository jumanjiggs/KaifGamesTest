using CodeBase.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Cubes
{
    public class CubePatrol : MonoBehaviour
    {
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed;
        [SerializeField] private float speedRotation;

        private int _indexWaypoint;
        private int _randomTiming;
        private float _timeElapsed;
        private float _timeValue;

        private void Start()
        {
            SetNewImpulseTiming();
        }

        private void Update()
        {
            GetCurrentWaypoint(out var waypoint);

            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _randomTiming)
            {
                MoveImpulseToWaypoint();
                _timeElapsed = 0f;
            }

            if (ReachedPoint(waypoint))
                IncreaseIndex();
            else
                MoveToWaypoint(waypoint);
        }

        private void RotateToWaypoint()
        {
            Quaternion lookRotation = Quaternion.LookRotation(waypoints[_indexWaypoint].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedRotation);
        }

        private void MoveToWaypoint(Transform waypoint)
        {
            ClampPositionCube();
            transform.position = Vector3.MoveTowards(transform.position, waypoint.position, speed * Time.deltaTime);
            RotateToWaypoint();
        }

        private void MoveImpulseToWaypoint()
        {
            rb.AddForce(waypoints[_indexWaypoint].position * 2f, ForceMode.Impulse);
            SetNewImpulseTiming();
        }

        private void ClampPositionCube()
        {
            var cubePosition = transform.position;
            cubePosition.x = Mathf.Clamp(transform.position.x, Constants.MinXClamp, Constants.MaxXClamp);
            cubePosition.z = Mathf.Clamp(transform.position.z, Constants.MinZClamp, Constants.MaxZClamp);
            transform.position = cubePosition;
        }

        private bool ReachedPoint(Transform waypoint) =>
            Vector3.Distance(transform.position, waypoint.position) < Constants.MinDistance;

        private void GetCurrentWaypoint(out Transform waypoint) =>
            waypoint = waypoints[_indexWaypoint];

        private void IncreaseIndex() =>
            _indexWaypoint = (_indexWaypoint + 1) % waypoints.Length;

        private void SetNewImpulseTiming() =>
            _randomTiming = Random.Range(Constants.MinTimeToImpulse, Constants.MaxTimeToImpulse);
    }
}