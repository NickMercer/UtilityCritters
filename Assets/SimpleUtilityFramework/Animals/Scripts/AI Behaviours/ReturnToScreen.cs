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
    [CreateAssetMenu(fileName = "New Return to Screen", menuName = "AI Behaviours/Return To Screen", order = 0)]
    public class ReturnToScreen : AIBehaviour
    {
        public override int Priority { get; } = 5;
        
        [SerializeField]
        private float _minSeconds = 2f;

        [SerializeField]
        private float _maxSeconds = 5f;
        
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            yield return new ActionTarget();
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target)
        {
            var screenBounds = GlobalBlackboard.Bounds;
            var position = blackboard.Self.transform.position;
            var offScreen = AIHelpers.IsOffScreen(position, screenBounds);
            
            return offScreen ? FloatNormal.One : FloatNormal.Zero;
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var wanderTime = Random.Range(_minSeconds, _maxSeconds);
            var screenBounds = GlobalBlackboard.Bounds;
            var transform = blackboard.Self.transform;
            var moveSpeed = Random.Range(0, blackboard.Animal.AnimalData.WalkSpeed / 10);
            
            var targetLocation = AIHelpers.CalculateMoveTarget(screenBounds.center, transform.position, moveSpeed, wanderTime);

            transform.DOMove(targetLocation, wanderTime);
            yield return new WaitForSeconds(wanderTime);
            
            onComplete?.Invoke();
        }
    }
}