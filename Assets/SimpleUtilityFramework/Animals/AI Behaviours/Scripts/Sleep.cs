using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using SimpleUtilityFramework.UtilitySystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(menuName = "AI Behaviours/Sleep", fileName = "New Sleep", order = 0)]
    public class Sleep : AIBehaviour
    {
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            yield return new ActionTarget();
        }

        public override float Score(AIBlackboard blackboard, ActionTarget target)
        {
            var currentEnergy = blackboard.Animal.Stats.Energy;
            var maxEnergy = blackboard.Animal.Stats.MaxEnergy;
            var energyScore = 1 - (currentEnergy / (float)maxEnergy);
            
            //Randomize a bit
            var randomScore = Random.Range(0.9f, 1.1f);
            
            return energyScore * randomScore;
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var transform = blackboard.Self.transform;
            transform.DORotate(new Vector3(-45, 0, 0), 1f);
            yield return new WaitForSeconds(1f);

            var sleepTime = Random.Range(6f, 10f);
            blackboard.Animal.Stats.RegenerateEnergy(sleepTime);
            var emote = Instantiate(GlobalBlackboard.EmotePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
            emote.Initialize(EmoteType.Sleep, sleepTime);
            yield return new WaitForSeconds(6f);

            transform.DORotate(Vector3.zero, 1f);
            yield return new WaitForSeconds(1f);
            onComplete?.Invoke();
        }
    }
}