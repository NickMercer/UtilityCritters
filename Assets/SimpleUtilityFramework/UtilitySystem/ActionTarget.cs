using System;
using UnityEngine;

namespace Natick.SimpleUtility
{
    [Serializable]
    public struct ActionTarget
    {
        public GameObject TargetObject;

        public Vector3 TargetLocation;
    }
}