using System.Collections;
using CodeBase.Helpers;
using UnityEngine;

namespace CodeBase.Cubes
{
    public class CubeHolder : MonoBehaviour
    {
        [SerializeField] private int minReviveTime;
        [SerializeField] private int maxReviveTime;
        
        private EventsHolder EventsHolder => EventsHolder.Instance;

        private void OnEnable() => 
            EventsHolder.killEvent.AddListener(OnCubeKill);

        private void OnDisable() => 
            EventsHolder?.killEvent.RemoveListener(OnCubeKill);

        private void OnCubeKill(GameObject cube)
        {
            cube.SetActive(false);
            StartCoroutine(ReviveCube(cube));
        }

        private IEnumerator ReviveCube(GameObject cube)
        {
            minReviveTime = 1;
            var randomSpawnTime = Random.Range(minReviveTime, maxReviveTime);
            yield return new WaitForSeconds(randomSpawnTime);
            cube.SetActive(true);
        }
    }
}