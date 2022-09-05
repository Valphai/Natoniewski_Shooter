using Chocolate4.Entities.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Chocolate4.UI
{
    public class PlayerHpBar : MonoBehaviour
    {
        private Slider bar;

        private void Awake()
        {
            bar = GetComponent<Slider>();
        }
        private void OnEnable()
        {
            PlayerHp.OnPlayerDamaged += UpdateHpBar;
        }
        private void OnDisable()
        {
            PlayerHp.OnPlayerDamaged -= UpdateHpBar;
        }
        private void UpdateHpBar(float currentNormalized)
        {
            bar.value = currentNormalized;
        }
    }
}