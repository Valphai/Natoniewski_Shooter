using System;
using System.Collections;
using System.Collections.Generic;
using Chocolate4.Entities;
using Chocolate4.Helpers;
using Chocolate4.Level;
using UnityEngine;

namespace Chocolate4.PersistantThroughLevels
{
    public class EntityManager : MonoBehaviour
    {
        public Enemy EnemyPrefab;
        [SerializeField] private List<Entity> entities;
        [SerializeField] private Factory<Entity> enemyFactory;
        private Player player;
        private const float _enemyRagdollDuration = 5f;
        public static event Action OnAllEnemiesKilled;

        private void OnValidate()
        {
            if (entities == null)
                entities = new List<Entity>();
        }
        private void OnEnable()
        {
            Entity.OnKilled += ReturnEntity;
            Spawner.OnSpawnRequest += SpawnEnemyAt;
            Player.OnPlayerJoin += RegisterPlayer;
            Player.OnPlayerKilled += ReturnAllEntities;
        }
        private void OnDisable()
        {
            Entity.OnKilled -= ReturnEntity;
            Spawner.OnSpawnRequest -= SpawnEnemyAt;
            Player.OnPlayerJoin -= RegisterPlayer;
            Player.OnPlayerKilled -= ReturnAllEntities;
        }
        private void LateUpdate()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].IsInitialized)
                    entities[i].UpdateEntity();
            }
        }
        public Entity SpawnEnemy()
        {
            if (enemyFactory == null)
            {
                enemyFactory = 
                    new Factory<Entity>(EnemyPrefab, sceneName:"PersistantScene");
            }

            Entity instance = enemyFactory.Get();
            
            if (player != null)
            {
                Enemy enemy = instance as Enemy;
                enemy.Player = player;
                enemy.Initialize();
            }
            
            entities.Add(instance);
            return instance;
        }
        public void SpawnEnemyAt(Vector3 p)
        {
            Entity instance = SpawnEnemy();
            instance.transform.position = p;
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
                    StartCoroutine(
                        ReturnEntityCo(entity)
                    );
                }
            }

            bool onlyPlayerLeft = entities.Count == 1;
            if (onlyPlayerLeft)
            {
                GameWon();
            }
        }
        private void ReturnAllEntities()
        {
            player = null;
            entities.Remove(player);
            entities.TrimExcess();
            foreach (Entity e in entities)
            {
                enemyFactory.Return(e);
            }
            entities.Clear();
        }
        private IEnumerator ReturnEntityCo(Entity entity)
        {
            yield return new WaitForSeconds(_enemyRagdollDuration);
            enemyFactory.Return(entity);
        }
        private void RegisterPlayer(Entity player)
        {
            this.player = (Player)player;
            foreach (Entity e in entities)
            {
                Enemy enemy = e as Enemy;
                enemy.Player = (Player)player;
                enemy.Initialize();
            }
            entities.Add(player);
        }
        private void GameWon()
        {
            Player p = entities[0] as Player;
            p.GameWon();

            entities.Remove(p);
            player = null;
            entities.TrimExcess();
            
            OnAllEnemiesKilled?.Invoke();
        }
    }
}