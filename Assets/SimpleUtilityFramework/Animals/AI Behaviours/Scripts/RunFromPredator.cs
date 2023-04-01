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
    [CreateAssetMenu(fileName = "New Run from Predator", menuName = "AI Behaviours/Run from Predator", order = 0)]
    public class RunFromPredator : AIBehaviour
    {
        [SerializeField]
        private Emote _emotePrefab;
        
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            //Get all animals within range
            foreach (var animal in AIHelpers.GetInRange<Animal>(5f, blackboard.Self.transform.position, 15))
            {
                if(animal.AnimalData.FoodSourceType == FoodType.Animal && animal != blackboard.Animal)
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
            //If we have no energy, don't run
            if (blackboard.Animal.Stats.Energy <= 20)
                return FloatNormal.Zero;
            
            //Randomize a bit so every animal doesn't always see the predator at the same time.
            return new FloatNormal(Random.Range(0.7f, 1f));
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var predatorLocation = target.TargetObject.transform.position;
            var transform = blackboard.Self.transform;
            var animalHealth = blackboard.Animal.Stats.Health / (float)blackboard.Animal.Stats.MaxHealth;
            var moveSpeed = blackboard.Animal.AnimalData.WalkSpeed/10 * animalHealth;

            // Basically, picks a point away from the predator to run to.
            var direction = transform.position - predatorLocation;
            var distanceToPredator = Vector3.Distance(transform.position, predatorLocation);
            var escapePointX = distanceToPredator * Mathf.Cos(Vector3.Angle(Vector3.up, direction));
            var escapePointY = distanceToPredator * Mathf.Sin(Vector3.Angle(Vector3.up, direction));
            var escapePoint = new Vector3(escapePointX, escapePointY, 0);
            var distance = Vector3.Distance(escapePoint, transform.position);
            var travelTime = distance / moveSpeed;
            
            AIHelpers.Emote(_emotePrefab, transform, EmoteType.Run);
            
            transform.DOMove(escapePoint, travelTime);
            yield return new WaitForSeconds(travelTime);
            
            onComplete?.Invoke();
        }
    }
}