using System.Collections.Generic;

namespace Natick.SimpleUtility
{
    public interface IPotentialAction
    {
        public IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard);

        public float Score(AIBlackboard blackboard, ActionTarget target);

        public void Act(AIBlackboard blackboard, ActionTarget target);
    }
}