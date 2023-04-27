using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using Natick.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Return to Spawn Point", menuName = "AI Behaviours/Return to Spawn Point", order = 0)]
    public class ReturnToSpawnPoint : AIBehaviour
    {
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            yield return new ActionTarget();
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target) => new FloatNormal(0.5f + Random.Range(-0.3f, 0.2f));

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var spawnPoint = blackboard.Animal.SpawnPoint;
            
            var speed = Random.Range(blackboard.Animal.AnimalData.WalkSpeed/15, blackboard.Animal.AnimalData.WalkSpeed/10);
            var transform = blackboard.Self.transform;
            var targetLocation = spawnPoint + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            var distance = Vector3.Distance(transform.position, targetLocation);
            var travelTime = distance / speed;
            
            transform.DOMove(targetLocation, travelTime);
            yield return new WaitForSeconds(travelTime);
            
            onComplete?.Invoke();
        }
    }
}