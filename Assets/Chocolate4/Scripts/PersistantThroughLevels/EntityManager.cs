using System.Collections.Generic;
using Chocolate4.Entities;
using Chocolate4.Helpers;
using UnityEngine;

namespace Chocolate4.PersistantThroughLevels
{
    public class EntityManager : MonoBehaviour
    {
        public Enemy EnemyPrefab;
        [SerializeField] private List<Entity> entities;
        [SerializeField] private Factory<Entity> enemyFactory;

        private void OnValidate()
        {
            if (entities == null)
                entities = new List<Entity>();
        }
        private void OnEnable()
        {
            Entity.OnKilled += ReturnEntity;
            Player.OnPlayerJoin += RegisterPlayer;
        }
        private void OnDisable()
        {
            Entity.OnKilled -= ReturnEntity;
            Player.OnPlayerJoin -= RegisterPlayer;
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
            enemyFactory=null;
            if (enemyFactory == null)
            {
                enemyFactory = 
                    new Factory<Entity>(EnemyPrefab, sceneName:"PersistantScene");
            }

            Entity instance = enemyFactory.Get();
            entities.Add(instance);
            return instance;
        }
        public void ReturnEntity(Entity entity)
        {
            if (enemyFactory == null)
            {
                enemyFactory = 
                    new Factory<Entity>(EnemyPrefab, sceneName:"PersistantScene");
            }

            if (entities.Contains(entity))
            {
                entities.Remove(entity);
                entities.TrimExcess();
                if (entity is Enemy)
                {
                    enemyFactory.Return(entity);
                }
            }
        }
        private void RegisterPlayer(Entity player)
        {
            foreach (Entity e in entities)
            {
                Enemy enemy = e as Enemy;
                enemy.Player = (Player)player;
                enemy.Initialize();
            }
            entities.Add(player);
        }
    }
}