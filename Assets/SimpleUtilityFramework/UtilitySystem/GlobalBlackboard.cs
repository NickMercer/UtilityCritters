using System;
using SimpleUtilityFramework.Environment;
using UnityEngine;

namespace Natick.SimpleUtility
{
    [Serializable]
    public class GlobalBlackboard : MonoBehaviour
    {
        [SerializeField]
        private FoodSourceList _activeFoodSources;
        public static FoodSourceList ActiveFoodSources => Instance._activeFoodSources;

        [SerializeField]
        private WaterSourceList _activeWaterSources;
        public static WaterSourceList ActiveWaterSources => Instance._activeWaterSources;

        [SerializeField]
        private Emote _emotePrefab;
        public static Emote EmotePrefab => Instance._emotePrefab;
        
        public static GlobalBlackboard Instance { get; private set; }
        
        private Rect _bounds;
        public Rect Bounds => _bounds;

        private void Awake()
        {
            var mainCamera = Camera.main;
            var cameraPos = mainCamera.transform.position;
            var cameraWidth = mainCamera.aspect * 2f * mainCamera.orthographicSize;
            var cameraHeight = 2f * mainCamera.orthographicSize;
            
            _bounds = new Rect(cameraPos.x - cameraWidth/2, cameraPos.y - cameraHeight/2, cameraWidth, cameraHeight);
            Instance = this;
        }
    }
}