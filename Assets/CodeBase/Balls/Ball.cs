using CodeBase.Helpers;
using UnityEngine;

namespace CodeBase.Balls
{
    public class Ball : MonoBehaviour
    {
        private EventsHolder EventsHolder => EventsHolder.Instance;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Constants.Plane)) 
                Destroy(gameObject);

            if (collision.gameObject.CompareTag(Constants.Cube)) 
                EventsHolder.killEvent.Invoke(collision.gameObject);
        }
    }
}