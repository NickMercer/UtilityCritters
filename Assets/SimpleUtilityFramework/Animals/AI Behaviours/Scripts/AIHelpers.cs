using UnityEngine;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    public static class AIHelpers
    {
        public static Vector3 CalculateMoveTarget(Vector3 goalPosition, Vector3 currentPosition, float movementSpeed, float movementTime)
        {
            var direction = goalPosition - currentPosition;
            var targetVector = direction.normalized * (movementSpeed * movementTime);
            var targetLocation = currentPosition + targetVector;
            return targetLocation;
        }
    }
}