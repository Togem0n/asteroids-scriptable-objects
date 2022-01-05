using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Variables;

namespace Ship
{
    public class Hull : MonoBehaviour
    {
        //[SerializeField] private IntVariable _health;
        [SerializeField] private ScriptableEventIntReference _onHealthChangedEvent;
        [SerializeField] private IntReference _healthRef;
        [SerializeField] private IntObservable _healthObservable;

        [SerializeField] private IntEvent onHealthChanged;

        [SerializeField] private Health health;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (string.Equals(other.gameObject.tag, "Asteroid"))
            {
                // TODO can we bake this into one call?
                //_healthRef.ApplyChange(-1);
                //_onHealthChangedEvent.Raise(_healthRef);
                //_healthObservable.ApplyChange(-1);

                health.TakeDamage(1); // modify health value 
                onHealthChanged.Raise(health.GetHealth); // display new health value
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(string.Equals(collision.gameObject.tag, "HealthPotion"))
            {
                health.HealthUp(1);
                onHealthChanged.Raise(health.GetHealth); // display new health value
            }
        }
    }
}
