using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener_Rotation : Tweener
{
    [System.Serializable]
    public class State
    {
        public float                m_Duration;
        public bool                 m_HasCurve;
        public AnimationCurve       m_Curve;
#if UNITY_EDITOR
        public float                m_Angle;
        public Vector3              m_Axis;
#endif
        public Quaternion           m_Rotation;
    }

    public List<State>              m_States;

    protected override void AwakeSpecific()
    {
        if (m_IsUI)
            m_UITransform = GetComponent<RectTransform>();
        else
            m_Transform = GetComponent<Transform>();
    }

    protected override void UpdateSpecific(float _Dt)
    {
        State currentState = m_States[m_CurrentStateId];
        State nextState = m_States[m_NextStateId];

        if (m_Time < 1.0f)
        {
            m_Time += _Dt / nextState.m_Duration;

            float animPercent = 0.0f;

            if (nextState.m_HasCurve)
                animPercent = nextState.m_Curve.Evaluate(m_Time);
            else
                animPercent = m_Time;

            if (m_IsUI)
                m_UITransform.localRotation = Quaternion.Lerp(currentState.m_Rotation, nextState.m_Rotation, animPercent);
            else
                m_Transform.localRotation = Quaternion.Lerp(currentState.m_Rotation, nextState.m_Rotation, animPercent);
        }
        else
            GoToNextState();
    }

    protected override int GetStateCount()
    {
        return m_States.Count;
    }
}
