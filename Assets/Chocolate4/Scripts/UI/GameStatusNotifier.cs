using Chocolate4.Entities;
using Chocolate4.PersistantThroughLevels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chocolate4.UI
{
    public class GameStatusNotifier : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI notification;
        [SerializeField] private GameObject mainPanel;
        private Image mainPanelBKG;
        
        private void Awake()
        {
            mainPanelBKG = mainPanel.GetComponent<Image>();
        }
        private void OnEnable()
        {
            Player.OnPlayerKilled += GameLost;
            EntityManager.OnAllEnemiesKilled += GameWon;
        }
        private void OnDisable()
        {
            Player.OnPlayerKilled -= GameLost;
            EntityManager.OnAllEnemiesKilled -= GameWon;
        }
        private void GameLost() => ShowTextOutBack("You lost!");
        private void GameWon() => ShowTextOutBack("Victory royale!");
        private void ShowTextOutBack(string text)
        {
            mainPanelBKG.color = new Color(0f, 0f, 0f, .35f);
            notification.text = text;
            LeanTween.moveY(
                notification.gameObject, 
                Screen.height * .5f, 
                1f
            ).setEaseInOutBack();
        }
    }
}