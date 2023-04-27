using System;
using System.Collections;
using System.Collections.Generic;
using Natick.SimpleUtility;
using Natick.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(menuName = "AI Behaviours/Sleep", fileName = "New Sleep", order = 0)]
    public class Sleep : AIBehaviour
    {
        public override int Priority { get; } = 2;
        
        [SerializeField]
        private Emote _emotePrefab;

        [SerializeField]
        private float _minSleepSeconds = 6f;

        [SerializeField]
        private float _maxSleepSeconds = 10f;

        [SerializeField]
        private int _energyPerSecond = 5;
        
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            yield return new ActionTarget();
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target)
        {
            var energyScore = 1 - blackboard.Animal.Stats.EnergyPercentage;
            
            //Randomize a bit
            var randomScore = Random.Range(0.9f, 1.1f);
            
            return new FloatNormal(energyScore * randomScore);
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var animal = blackboard.Animal;
            var transform = blackboard.Self.transform;
            
            animal.AnimateSleeping(1f);
            yield return new WaitForSeconds(1f);

            var sleepTime = Random.Range(_minSleepSeconds, _maxSleepSeconds);
            blackboard.Animal.Stats.RegenerateEnergy(sleepTime, _energyPerSecond);

            AIHelpers.Emote(_emotePrefab, transform, EmoteType.Sleep);
            yield return new WaitForSeconds(sleepTime);

            animal.ResetAnimation(1f);
            yield return new WaitForSeconds(1f);
            
            onComplete?.Invoke();
        }
    }
}