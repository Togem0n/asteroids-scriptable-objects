using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Variables;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        //[SerializeField] private ScriptableEventInt _onAsteroidDestroyed;
        [SerializeField] private IntEvent onAsteroidDestoryed;
        
        [Header("Config:")]
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _minTorque;
        [SerializeField] private float _maxTorque;

        [Header("References:")]
        [SerializeField] private Transform _shape;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private int _instanceId;
        private float size;

        [SerializeField] private AsteroidSet asteroidSet;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _instanceId = GetInstanceID();
            SetDirection();
            AddForce();
            AddTorque();
            //SetSize();
            asteroidSet.Add(_instanceId, this);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (string.Equals(other.tag, "Laser"))
            {
               HitByLaser();
            }
        }

        private void HitByLaser()
        {
            onAsteroidDestoryed.Raise(_instanceId);
            Destroy(gameObject);
        }

        // TODO Can we move this to a single listener, something like an AsteroidDestroyer?
        public void OnHitByLaser(IntReference asteroidId)
        {
            if (_instanceId == asteroidId.GetValue())
            {
                Destroy(gameObject);
            }
        }
        
        public void OnHitByLaserInt(int asteroidId)
        {
            if (_instanceId == asteroidId)
            {
                Destroy(gameObject);
            }
        }
        
        private void SetDirection()
        {
            var size = new Vector2(3f, 3f);
            var target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce()
        {
            var force = Random.Range(_minForce, _maxForce);
            _rigidbody.AddForce( _direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque()
        {
            var torque = Random.Range(_minTorque, _maxTorque);
            var roll = Random.Range(0, 2);

            if (roll == 0)
                torque = -torque;
            
            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        public void SetSize(bool isSplited)
        {
            if (!isSplited)
            {
                size = Random.Range(_minSize, _maxSize);
                _shape.localScale = new Vector3(size, size, 0f);
            }
        }
    }
}
