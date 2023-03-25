using System;
using UnityEngine;

namespace Natick.SimpleUtility
{
    [Serializable]
    public class ActionTarget<T> : ActionTarget
    {
        public T Target;
    }

    [Serializable]
    public class ActionTarget
    {
        public GameObject TargetObject;

        public Vector3 TargetLocation;
    }
}