using UnityEngine;

namespace Chocolate4.PersistantThroughLevels
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public AudioClip soundClip;
        private AudioSource source;
        
        private void Awake()
        {
            source = GetComponent<AudioSource>();
        }
        private void OnEnable() => EntityManager.OnAllEnemiesKilled += GameWin;
        private void OnDisable() => EntityManager.OnAllEnemiesKilled -= GameWin;
        private void GameWin()
        {
            source.clip = soundClip;
            source.Play();
        }
    }
}