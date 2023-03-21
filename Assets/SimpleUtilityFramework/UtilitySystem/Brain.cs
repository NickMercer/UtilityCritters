using System.Collections.Generic;
using UnityEngine;

namespace Natick.SimpleUtility
{
    public class Brain : MonoBehaviour
    {
        [SerializeField]
        private AIBlackboard _blackboard;

        [SerializeField]
        private List<IPotentialAction> _potentialActions;
        
        public ActionSelection Decide()
        {
            ActionSelection topSelection = default;
            foreach (var action in _potentialActions)
            {
                var targets = action.GetTargets(_blackboard);
                foreach (var target in targets)
                {
                    var score = action.Score(_blackboard, target);
                    if (score > topSelection.Score)
                    {
                        topSelection = new ActionSelection(action, target, score);
                    }
                }
            }

            return topSelection;
        }
    }
}