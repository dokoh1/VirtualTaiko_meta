using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Single
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private List<SceneData> sceneObjects;

        [SerializeField] private Image fadeImage;
        private readonly float duration = 1f;

        private GameObject currentScene;
        private GameObject nextScene;
        private Tween fadeTween;
        private float _lastHitTime = float.MinValue;
        private float _ignoreDuration = 0.1f;
        private bool _isGameStart = false;
        public TestDrumInput testDrumInput;
        private void Awake()
        {
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;
        }

        private void Update()
        {
            if (!_isGameStart)
            {
                // DrumDataType drumDataType = System.DrumManager.UseQueue();
                // if (drumDataType.testInputType != DrumDataType.NotHit)
                if (testDrumInput.testInputType != DrumDataType.NotHit)
                {
                    _isGameStart = true;
                    foreach (SceneData sceneData in sceneObjects)
                    {
                        if (sceneData.SceneDataType == SceneDataType.Start)
                        {
                            sceneData.SceneObject.SetActive(true);
                            currentScene = sceneData.SceneObject;
                        }
                        else
                            sceneData.SceneObject.SetActive(false);
                    }
                }
            }
        }
        
        //검은 화면으로 천천히 덮기
        public void FadeOut(float duration, Action onComplete = null)
        {
            fadeImage.gameObject.SetActive(true);
            fadeTween = fadeImage.DOFade(1f, duration).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
        
        //검은 화면으로 천천히 걷어내기
        public void FadeIn(float duration, Action onComplete = null)
        {
            fadeTween = fadeImage.DOFade(0f, duration).OnComplete(() =>
            {
                fadeImage.gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }
        
        public void LoadScene(SceneDataType sceneDataType)
        {
            foreach (SceneData sceneData in sceneObjects)
            {
                if (sceneData.SceneDataType == sceneDataType)
                    nextScene = sceneData.SceneObject;
                else
                    if (sceneData.SceneObject.activeSelf == true)
                        currentScene = sceneData.SceneObject;
            }
            FadeOut(1.5f, () =>
            {
                currentScene.SetActive(false);
                nextScene.SetActive(true);
                FadeIn(1.5f, () =>
                {
                    fadeImage.gameObject.SetActive(false);
                });
            });
        }
    }
}

