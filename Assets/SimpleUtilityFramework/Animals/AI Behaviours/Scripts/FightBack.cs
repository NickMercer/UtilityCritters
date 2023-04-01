using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using SimpleUtilityFramework.Environment;
using SimpleUtilityFramework.UtilitySystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Fight Back", menuName = "AI Behaviours/Fight Back", order = 0)]
    public class FightBack : AIBehaviour
    {
        [SerializeField]
        private float _fightingSeconds = 1f;

        [SerializeField]
        private int _minDamage = 10;

        [SerializeField]
        private int _maxDamage = 20;
        
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            foreach (var animal in AIHelpers.GetInRange<Animal>(0.5f, blackboard.Self.transform.position))
            {
                if (animal.AnimalData.FoodSourceType == FoodType.Animal && animal.gameObject != blackboard.Animal.gameObject)
                {
                    yield return new ActionTarget<Animal>
                    {
                        Target = animal,
                        TargetLocation = animal.transform.position,
                        TargetObject = animal.gameObject
                    };
                }
            }
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target)
        {
            var animal = ((ActionTarget<Animal>) target).Target;
            var predatorHealth = animal.Stats.Health;
            if (predatorHealth == 0)
                return FloatNormal.Zero;
            
            var preyHealth = blackboard.Animal.Stats.Health;
            var fightBackScore = preyHealth / predatorHealth;
            
            return new FloatNormal(fightBackScore * Random.Range(0.7f, 1f));
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var animal = blackboard.Animal;
            var predator = ((ActionTarget<Animal>) target).Target;

            animal.AnimateEating(_fightingSeconds);
            yield return new WaitForSeconds(_fightingSeconds/2);
            
            predator.Stats.UpdateHealth(-Random.Range(_minDamage, _maxDamage));
            
            yield return new WaitForSeconds(_fightingSeconds/2);
            onComplete?.Invoke();
        }
    }
}