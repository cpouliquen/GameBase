using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tweener_Scale))]
public class Tweener_ScaleEditor : Editor
{
    private Tweener_Scale m_Target;

    void OnEnable()
    {
        m_Target = (Tweener_Scale)target;
    }

    public override void OnInspectorGUI()
    {
        TweenerEditor.DrawEditor(m_Target);
        DrawEditor(m_Target);
    }

    public static void DrawEditor(Tweener_Scale _Target)
    {
        if (_Target.m_States == null)
            _Target.m_States = new List<Tweener_Scale.State>();

        for (int i=0; i < _Target.m_States.Count; ++i)
        {
            DrawStateEditor(_Target, i);
            if (GUILayout.Button("-"))
            {
                _Target.m_States.RemoveAt(i);
                return;
            }
        }

        if (GUILayout.Button("+"))
            _Target.m_States.Add(new Tweener_Scale.State());
    }

    public static void DrawStateEditor(Tweener_Scale _Target, int _StateId)
    {
        Tweener_Scale.State state = _Target.m_States[_StateId];

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
                state.m_Scale = EditorGUILayout.Vector2Field("Scale", state.m_Scale);
            else
                state.m_Scale = EditorGUILayout.Vector3Field("Scale", state.m_Scale);
        }
        else if (!Application.isPlaying)
        {
            if (_Target.m_IsUI)
            {
                RectTransform targetTr = _Target.GetComponent<RectTransform>();
                if (targetTr != null)
                    EditorGUILayout.Vector2Field("Start size", targetTr.sizeDelta);
            }
            else
            {
                Transform targetTr = _Target.GetComponent<Transform>();
                if (targetTr != null)
                    EditorGUILayout.Vector3Field("Start size", targetTr.localScale);
            }
        } 
    }
}
