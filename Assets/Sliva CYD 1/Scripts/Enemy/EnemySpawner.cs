using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        // Start is called before the first frame update
        void Start()
        {
            SpawnEnemy();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SpawnEnemy()
        {
            GameObject spwEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

            EnemyTracker tracker = spwEnemy.GetComponent<EnemyTracker>();
            if (tracker == null)
            {
                tracker = spwEnemy.AddComponent<EnemyTracker>();
            }
            tracker.Spawner = this;
        }

        public void OnEnemyDestroy()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(2f);
            
            SpawnEnemy();
        }
    }
}
