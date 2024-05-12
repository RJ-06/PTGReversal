using System;
using UnityEngine;

namespace beats
{
    public class AudioBinder : MonoBehaviour
    {
        private const int SampleCount = 1024;
        private const float ReferenceValue = 0.1f;
        private const float DBSize = 120;
        private float[] _samples;
        [NonSerialized] public float Value;

        private void Start()
        {
            _samples = new float[SampleCount];
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            var sum = 0f;
            for (var i = 0; i < data.Length; i++)
            {
                sum += data[i] * data[i];
            }
            var rms = Mathf.Sqrt(sum / data.Length);
            var db = 20 * Mathf.Log10(rms / ReferenceValue);
            Value = Mathf.Clamp01((db) / DBSize);
        }
    }
}