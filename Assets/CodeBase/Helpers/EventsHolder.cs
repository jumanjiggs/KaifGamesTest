using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Helpers
{
    public class EventsHolder : Singleton<EventsHolder>
    {
        public UnityEvent<GameObject> killEvent;
        public UnityEvent spawnBall;
        public UnityEvent destroyBall;
    }
}