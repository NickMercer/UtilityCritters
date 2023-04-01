using System.Collections.Generic;
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

        public static IEnumerable<T> GetInRange<T>(float radius, Vector3 startPosition, int max = 5) where T : Component
        {
            var colliders = new Collider2D[max];
            var size = Physics2D.OverlapCircleNonAlloc(startPosition, radius, colliders);
            for (var i = 0; i < size; i++)
            {
                var collider = colliders[i];
                if(collider.TryGetComponent<T>(out var component))
                    yield return component;
            }
        }

        public static void Emote(Emote emotePrefab, Transform parent, EmoteType emoteType)
        {
            var emote = GameObject.Instantiate(emotePrefab, parent.position + new Vector3(0, 1, 0), Quaternion.identity, parent);
            emote.Initialize(emoteType);
        }
    }
}