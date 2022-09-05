using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Chocolate4.Helpers;
using System;

namespace Chocolate4.UI.MainMenu
{
    public class MMController : MonoBehaviour
    {
        [SerializeField] private MMModel model;
        [SerializeField] private Image[] bkgImages;
        [SerializeField] private GameObject buttonsContainer;
        private int imgIndex;
        private const float _imagePresentTime = 10f;
        private const float _fadeTime = 2f;
        private const float _imageOffsetFromCenter = 30f;
        public static event Action OnClickPlay;

        private void Start()
        {
            ShowButtons();

            StartCoroutine(
                NextImageCo()
            );
        }
        public void Play()
        {
            OnClickPlay?.Invoke();
        }
        public void Quit()
        {
            Application.Quit();
        }
        private IEnumerator NextImageCo()
        {
            imgIndex = imgIndex % 2;
            int nextIndex = (imgIndex + 1) % 2;

            Vector2 screenCenter = new Vector2(Screen.width * .5f, Screen.height * .5f);
            Vector2 startPos = 
                screenCenter + UnityEngine.Random.insideUnitCircle * _imageOffsetFromCenter;
            Vector2 endPos = 
                screenCenter + UnityEngine.Random.insideUnitCircle * _imageOffsetFromCenter;

            Sprite nextSprite;
            bool queueEmpty = !model.SpritesQueue.TryDequeue(out nextSprite);
            if (queueEmpty) yield break;

            bkgImages[imgIndex].sprite = nextSprite;

            bkgImages[imgIndex].FadeIn(_fadeTime);
            bkgImages[nextIndex].FadeOut(_fadeTime);

            bkgImages[imgIndex].rectTransform.position = startPos;
            LeanTween.move(bkgImages[imgIndex].gameObject, endPos, _imagePresentTime);

            yield return new WaitForSeconds(_imagePresentTime);
            imgIndex++;

            model.SpritesQueue.Enqueue(nextSprite);
            StartCoroutine(
                NextImageCo()
            );
        }
        private void ShowButtons()
        {
            Vector3 startOffset = new Vector3(250f, 0f, 0f);
            buttonsContainer.transform.position -= startOffset;
            LeanTween.moveX(buttonsContainer, 0f, 1f).setEaseInOutExpo();
        }
    }
}