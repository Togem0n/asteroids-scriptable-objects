using System;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidDestroyer : MonoBehaviour
    {
        [SerializeField] private AsteroidSet _asteroids;
        [SerializeField] private AsteroidSpawner asteroidSpawner;
        [SerializeField] private GameObject _asteroidPrefab;

        private int numOfSplited = 4;

        public void OnAsteroidHitByLaser(int asteroidId)
        {
            Asteroid tmp = _asteroids.Get(asteroidId);

            float size = tmp.transform.Find("Asteroid_Size").localScale.x;

            if(size > 0.3f)
            {
                RegisterAsteroid(tmp, size);
            }
      
            DestroyAsteroid(tmp, asteroidId);
        }

        public void RegisterAsteroid(Asteroid asteroid, float size)
        {
            for (int i = 0; i < numOfSplited; i++)
            {
                GameObject go = Instantiate(_asteroidPrefab, asteroid.transform.position, Quaternion.identity) as GameObject;
                go.transform.Find("Asteroid_Size").localScale = new Vector3(size / 2, size / 2, 0f);
                _asteroids.Add(go.GetInstanceID(), go.GetComponent<Asteroid>());
            }
        }

        private void DestroyAsteroid(Asteroid asteroid, int asteroidId)
        {
            _asteroids.Remove(asteroidId);
            Destroy(asteroid.gameObject);
        }
    }
}
