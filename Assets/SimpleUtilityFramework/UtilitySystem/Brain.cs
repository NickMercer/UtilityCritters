using System.Collections.Generic;
using SimpleUtilityFramework.UtilitySystem;
using UnityEngine;

namespace Natick.SimpleUtility
{
    public class Brain : MonoBehaviour
    {
        [SerializeField]
        private AIBlackboard _blackboard;
        public AIBlackboard Blackboard => _blackboard;
        
        [SerializeField]
        private List<AIBehaviour> _potentialActions;

        [SerializeField, ShowOnly]
        private List<ActionSelection> _lastScores = new List<ActionSelection>();
        
        public void Initialize(Animal animal)
        {
            _blackboard = new AIBlackboard(animal);
            _potentialActions = animal.AnimalData.AIBehaviours;
        }
        
        public ActionSelection Decide()
        {
            _lastScores.Clear();
            ActionSelection topSelection = default;
            foreach (var action in _potentialActions)
            {
                var targets = action.GetTargets(_blackboard);
                foreach (var target in targets)
                {
                    var score = action.Score(_blackboard, target);
                    var selection = new ActionSelection(action, target, score);
                    _lastScores.Add(selection);
                    
                    if (score > topSelection.Score)
                    {
                        topSelection = selection;
                    }
                }
            }

            return topSelection;
        }
    }
}