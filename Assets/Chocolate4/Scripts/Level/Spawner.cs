using System;
using UnityEngine;

namespace Chocolate4.Level
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int howManyToSpawnLow;
        [SerializeField] private int howManyToSpawnHigh;
        [SerializeField] private GameObject groundPlane;
        private Vector2 spawnArea;
        private const float _spawnOffset = .3f;
        public static event Action<Vector3> OnSpawnRequest;

        private void OnValidate()
        {
            howManyToSpawnLow = Math.Min(
                howManyToSpawnLow, howManyToSpawnHigh
            );
            howManyToSpawnHigh = Math.Max(
                howManyToSpawnLow, howManyToSpawnHigh
            );
            howManyToSpawnLow = Math.Clamp(howManyToSpawnLow, 0, 30);
            howManyToSpawnHigh = Math.Clamp(howManyToSpawnHigh, 0, 60);
        }
        private void Awake()
        {
            Renderer planeRenderer = groundPlane.GetComponent<Renderer>();
            Vector3 area =  Vector3.Scale(
                planeRenderer.bounds.size, 
                groundPlane.transform.localScale
            );
            spawnArea = new Vector2(area.x, area.z);
        }
        private void Start()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            int i = 0;
            int spawnCount = UnityEngine.Random.Range(howManyToSpawnLow, howManyToSpawnHigh);
            while (i < spawnCount)
            {
                Vector3 spawnPoint = new Vector3(
                    UnityEngine.Random.Range(spawnArea.x, -spawnArea.x),
                    10f,
                    UnityEngine.Random.Range(spawnArea.y, -spawnArea.y)
                );

                Ray r = new Ray(spawnPoint, Vector3.down);
                if (Physics.Raycast(r, out RaycastHit hitInfo))
                {
                    Vector3 p = hitInfo.point;
                    if (p.y <= _spawnOffset)
                    {
                        OnSpawnRequest?.Invoke(p);
                        i++;
                    }
                }
            }
        }
    }
}