using System;
using SimpleUtilityFramework.Environment;
using UnityEngine;

namespace Natick.SimpleUtility
{
    [Serializable]
    public class GlobalBlackboard : MonoBehaviour
    {
        private static GlobalBlackboard _instance;
        
        [SerializeField]
        private FoodSourceList _activeFoodSources;
        public static FoodSourceList ActiveFoodSources => _instance._activeFoodSources;

        [SerializeField]
        private WaterSourceList _activeWaterSources;
        public static WaterSourceList ActiveWaterSources => _instance._activeWaterSources;
        
        private Rect _bounds;
        public static Rect Bounds => _instance._bounds;

        private void Awake()
        {
            var mainCamera = Camera.main;
            var cameraPos = mainCamera.transform.position;
            var cameraWidth = mainCamera.aspect * 2f * mainCamera.orthographicSize;
            var cameraHeight = 2f * mainCamera.orthographicSize;
            
            _bounds = new Rect(cameraPos.x - cameraWidth/2, cameraPos.y - cameraHeight/2, cameraWidth, cameraHeight);
            _instance = this;
        }
    }
}