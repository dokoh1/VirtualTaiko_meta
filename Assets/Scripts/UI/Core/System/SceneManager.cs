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
        
        private Tween fadeTween;

        private void Awake()
        {
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;
            foreach (SceneData sceneData in sceneObjects)
            {
                if (sceneData.SceneDataType == SceneDataType.Start)
                    sceneData.SceneObject.SetActive(true);
                else
                    sceneData.SceneObject.SetActive(false);
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
            GameObject previousScene = null;
            GameObject targetScene = null;
            
            foreach (SceneData sceneData in sceneObjects)
            {
                if (sceneData.SceneObject.activeSelf)
                    previousScene = sceneData.SceneObject;
                if (sceneData.SceneDataType == sceneDataType)
                {
                    targetScene = sceneData.SceneObject;
                }
            }
            if (targetScene == null)
            {
                Debug.LogError($"Scene data type {sceneDataType} not found");
                return;
            }

            if (previousScene == null)
            {
                Debug.LogError($"Scene data type {sceneDataType} not found");
            }
            if (previousScene != null && previousScene != targetScene)
            {
                FadeOut(1.5f, () =>
                {
                    previousScene.SetActive(false);
                    targetScene.SetActive(true);
                    FadeIn(1.5f, () =>
                    {
                        fadeImage.gameObject.SetActive(false);
                    });
                });
            }
            else if (previousScene == null)
            {
                targetScene.SetActive(true);
            }
        }
    }
}

