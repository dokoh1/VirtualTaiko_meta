using System;
using System.Collections.Generic;
using UnityEngine;

namespace dokoh
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private List<SceneData> sceneObjects;

        private void Start()
        {
            foreach (SceneData sceneData in sceneObjects)
            {
                // if (sceneData.SceneDataType == SceneDataType.Start)
                if (sceneData.SceneDataType == SceneDataType.Music1)
                    sceneData.SceneObject.SetActive(true);
                else
                    sceneData.SceneObject.SetActive(false);
            }
        }

        public void LoadScene(SceneDataType sceneDataType)
        {
            foreach (SceneData sceneData in sceneObjects)
            {
                if (sceneData.SceneDataType == sceneDataType)
                    sceneData.SceneObject.SetActive(true);
                else
                    sceneData.SceneObject.SetActive(false);
            }
        }
    }
}

