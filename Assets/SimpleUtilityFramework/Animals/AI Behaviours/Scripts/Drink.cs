using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using SimpleUtilityFramework.Environment;
using SimpleUtilityFramework.UtilitySystem;
using UnityEngine;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Drink", menuName = "AI Behaviours/Drink", order = 0)]
    public class Drink : AIBehaviour
    {
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            //Get all foods within eating range (1f)
            var colliders = new Collider2D[5];
            var size = Physics2D.OverlapCircleNonAlloc(blackboard.Self.transform.position, 1f, colliders);
            for (var i = 0; i < size; i++)
            {
                var collider = colliders[i];
                var water = collider.GetComponent<WaterSource>();
                if (water != null)
                {
                    yield return new ActionTarget<WaterSource>
                    {
                        Target = water,
                        TargetLocation = water.transform.position,
                        TargetObject = water.gameObject
                    };
                }
            }
        }

        public override float Score(AIBlackboard blackboard, ActionTarget target)
        {
            var water = (target as ActionTarget<WaterSource>).Target;
            if (water.IsAvailable == false)
                return 0f;

            var currentThirst = blackboard.Animal.Stats.Thirst;
            var maxThirst = blackboard.Animal.Stats.MaxThirst;
            var thirstScore = currentThirst / (float)maxThirst;
            var waterQuality = water.ThirstToQuench / 10f;
            
            return thirstScore * waterQuality;
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var water = (target as ActionTarget<WaterSource>).Target;
            
            blackboard.Self.transform.DORotate(new Vector3(0, 0, -30), 2f);
            water.Occupy();
            yield return new WaitForSeconds(2f);
            
            water.Consume(blackboard.Animal);
            
            blackboard.Self.transform.DORotate(Vector3.zero, 1f);
            yield return new WaitForSeconds(1f);
            water.Vacate();
            
            onComplete?.Invoke();
        }
    }
}