using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Tweener_Position))]
public class Tweener_PositionEditor : Editor
{
    private Tweener_Position m_Target;

    void OnEnable()
    {
        m_Target = (Tweener_Position)target;
    }

    public override void OnInspectorGUI()
    {
        TweenerEditor.DrawEditor(m_Target);
        DrawEditor(m_Target);
    }

    public static void DrawEditor(Tweener_Position _Target)
    {
        if (_Target.m_States == null)
            _Target.m_States = new List<Tweener_Position.State>();

        for (int i = 0; i < _Target.m_States.Count; ++i)
        {
            DrawStateEditor(_Target, i);
            if (GUILayout.Button("-"))
            {
                _Target.m_States.RemoveAt(i);
                return;
            }
        }

        if (GUILayout.Button("+"))
            _Target.m_States.Add(new Tweener_Position.State());
    }

    public static void DrawStateEditor(Tweener_Position _Target, int _StateId)
    {
        Tweener_Position.State state = _Target.m_States[_StateId];

        state.m_Duration = EditorGUILayout.FloatField("Duration", state.m_Duration);
        state.m_HasCurve = EditorGUILayout.Toggle("Has curve", state.m_HasCurve);
        if (state.m_HasCurve)
        {
            if (state.m_Curve == null)
                state.m_Curve = new AnimationCurve();

            state.m_Curve = EditorGUILayout.CurveField("Curve", state.m_Curve);
        }

        if (_StateId != 0 || _Target.m_OverrideStartState)
        {
            if (_Target.m_IsUI)
                state.m_Position = EditorGUILayout.Vector2Field("Position", state.m_Position);
            else
                state.m_Position = EditorGUILayout.Vector3Field("Position", state.m_Position);
        }
        else if (!Application.isPlaying)
        {
            if (_Target.m_IsUI)
            {
                RectTransform targetTr = _Target.GetComponent<RectTransform>();
                if (targetTr != null)
                    EditorGUILayout.Vector2Field("Start pos", targetTr.anchoredPosition);
            }
            else
            {
                Transform targetTr = _Target.GetComponent<Transform>();
                if (targetTr != null)
                    EditorGUILayout.Vector3Field("Start pos", targetTr.localPosition);
            }
        }
    }
}
