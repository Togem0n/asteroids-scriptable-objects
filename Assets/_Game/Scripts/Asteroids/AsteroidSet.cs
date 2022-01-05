using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(fileName = "new AsteroidSet", menuName = "ScriptableObjects/AsteroidSet")]
    public class AsteroidSet : ScriptableObject
    {
        private Dictionary<int, Asteroid> _asteroids = new Dictionary<int, Asteroid>();

        private void Awake()
        {
            Clear();
        }

        public void Add(int id, Asteroid asteroid)
        {
            _asteroids.Add(id, asteroid);
        }

        public void Remove(int id)
        {
            _asteroids.Remove(id);
        }

        public Asteroid Get(int id)
        {
            return _asteroids[id];
        }

        private void Clear()
        {
            _asteroids = new Dictionary<int, Asteroid>();
        }
    }
}
