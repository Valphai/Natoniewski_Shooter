using System.Collections.Generic;
using UnityEngine;

namespace Chocolate4.UI.MainMenu
{
    public class MMModel : MonoBehaviour
    {
        public Queue<Sprite> SpritesQueue { get; private set; }
        [SerializeField] private Sprite[] bkgSprites;
        
        private void Awake()
        {
            SpritesQueue = new Queue<Sprite>();
            foreach (Sprite s in bkgSprites)
            {
                SpritesQueue.Enqueue(s);
            }
        }
    }
}