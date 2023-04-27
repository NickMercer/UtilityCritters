using System.Collections.Generic;
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

        private ActionSelection _lastSelection = new ActionSelection();

        public void Initialize(Animal animal)
        {
            _blackboard = new AIBlackboard(animal);
            _potentialActions = animal.AnimalData.AIBehaviours;
        }
        
        public ActionSelection Decide()
        {
            _lastScores.Clear();
            _blackboard.LastSelection = _lastSelection;
            
            ActionSelection topSelection = default;
            foreach (var action in _potentialActions)
            {
                var targets = action.GetTargets(_blackboard);
                foreach (var target in targets)
                {
                    var score = action.Score(_blackboard, target);
                    var selection = new ActionSelection(action, target, score);
                    _lastScores.Add(selection);
                    
                    if (score.Value > topSelection.Score.Value || (score.Value == topSelection.Score.Value && selection.Action.Priority >= topSelection.Action.Priority))
                    {
                        topSelection = selection;
                    }
                }
            }

            _lastSelection = topSelection;
            return topSelection;
        }
    }
}