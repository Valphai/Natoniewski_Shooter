using System.Collections.Generic;
using Chocolate4.Entities;
using Chocolate4.Helpers;
using UnityEngine;

namespace Chocolate4.PersistantThroughLevels
{
    public class EntityManager : MonoBehaviour
    {
        public Entity EnemyPrefab;
        private List<Entity> entities;
        private Factory<Entity> enemyFactory;

        private void OnEnable() => Player.OnPlayerJoin += AddEntity;
        private void OnDisable() => Player.OnPlayerJoin -= AddEntity;
        private void Awake()
        {
            entities = new List<Entity>();
            enemyFactory = 
                new Factory<Entity>(EnemyPrefab);
        }
        private void LateUpdate()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].UpdateEntity();
            }
        }
        public Entity SpawnEnemy()
        {
            if (enemyFactory == null)
            {
                enemyFactory = 
                    new Factory<Entity>(EnemyPrefab);
            }

            Entity instance = enemyFactory.Get();
            entities.Add(instance);
            instance.Initialize();
            return instance;
        }
        public void ReturnEnemy(Entity enemy)
        {
            if (entities.Contains(enemy))
            {
                entities.Remove(enemy);
                enemyFactory.Return(enemy);
            }
        }
        private void AddEntity(Entity entity) => entities.Add(entity);
    }
}