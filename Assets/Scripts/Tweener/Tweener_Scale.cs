using System.Collections.Generic;
using UnityEngine;

public class Tweener_Scale : Tweener
{
    [System.Serializable]
    public class State
    {
        public float            m_Duration;
        public bool             m_HasCurve;
        public AnimationCurve   m_Curve;
        public Vector3          m_Scale;
    }

    public List<State>          m_States;

    protected override void AwakeSpecific()
    {
        if (m_IsUI)
        {
            m_UITransform = GetComponent<RectTransform>();
            if (!m_OverrideStartState)
                m_States[0].m_Scale = m_UITransform.sizeDelta;
        } 
        else
        {
            m_Transform = GetComponent<Transform>();
            if (!m_OverrideStartState)
                m_States[0].m_Scale = m_Transform.localScale;
        }
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
                m_UITransform.sizeDelta = Vector2.Lerp(currentState.m_Scale, nextState.m_Scale, animPercent);
            else
                m_Transform.localScale = Vector3.Lerp(currentState.m_Scale, nextState.m_Scale, animPercent);
        }
        else
            GoToNextState();
    }

    protected override int GetStateCount()
    {
        return m_States.Count;
    }
}
