using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class EnemyTracker : MonoBehaviour
    {
        public EnemySpawner Spawner { get; set; }

        private void OnDestroy()
        {
            if (Spawner != null && !gameObject.scene.isLoaded)
            {
                Spawner.OnEnemyDestroy();
            }
        }
    }
    
}
