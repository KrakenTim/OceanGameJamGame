using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RotatingIndicatorHandler : MonoBehaviour
{
    [Tooltip("Angle where indicator needles is max"), SerializeField] private float AngleTimeMax;
    [Tooltip("Angle where indicator needles is min"), SerializeField] private float AngleTimeMin;

    [SerializeField] private Transform Indicator;
    [SerializeField, Range(0, 1)] private float slerpSpeed;

    private float DeltaAngleIndicator;

    private void Start()
    {
        DeltaAngleIndicator = (AngleTimeMax - AngleTimeMin);
    }

    /// <summary>
    /// Rotates the Indicator to the Rotation that corresponds to the passed normalized Value.
    /// </summary>
    /// <param name="normalizedIndicatedValue">clamped 0 1</param>
    public void IndicateNormalizedTime(float normalizedIndicatedValue)
    {
        normalizedIndicatedValue = Mathf.Clamp01(normalizedIndicatedValue);
        Indicator.rotation = Quaternion.Slerp(Indicator.rotation, Quaternion.Euler(0, 0, AngleTimeMin + normalizedIndicatedValue * DeltaAngleIndicator), slerpSpeed);
    }
}
