using System;
using System.Globalization;
using UnityEngine;

namespace Natick.Utilities
{
    [Serializable]
    public struct FloatNormal
    {
        public static FloatNormal Zero = new FloatNormal(0f);
        public static FloatNormal One = new FloatNormal(1f);

        [field: SerializeField, ShowOnly]
        private float _value;
        public float Value => _value;

        public FloatNormal(float value, float minValue = 0f, float maxValue = 1f)
        {
            if (maxValue - minValue != 0)
                _value = Mathf.Clamp01((value - minValue) / (maxValue - minValue));
            else
                _value = 0;
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}