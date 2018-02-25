using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Tweener_Rotation))]
public class Tweener_RotationEditor : Editor
{
    private Tweener_Rotation m_Target;

    void OnEnable()
    {
        m_Target = (Tweener_Rotation)target;
    }

    public override void OnInspectorGUI()
    {
        TweenerEditor.DrawEditor(m_Target);
        DrawEditor(m_Target);
    }

    public static void DrawEditor(Tweener_Rotation _Target)
    {
        if (_Target.m_States == null)
            _Target.m_States = new List<Tweener_Rotation.State>();

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
            _Target.m_States.Add(new Tweener_Rotation.State());
    }

    public static void DrawStateEditor(Tweener_Rotation _Target, int _StateId)
    {
        Tweener_Rotation.State state = _Target.m_States[_StateId];

        state.m_Duration = EditorGUILayout.FloatField("Duration", state.m_Duration);
        state.m_HasCurve = EditorGUILayout.Toggle("Has curve", state.m_HasCurve);
        if (state.m_HasCurve)
        {
            if (state.m_Curve == null)
                state.m_Curve = new AnimationCurve();

            state.m_Curve = EditorGUILayout.CurveField("Curve", state.m_Curve);
        }

        state.m_Angle = EditorGUILayout.FloatField("Angle", state.m_Angle);
        state.m_Axis = EditorGUILayout.Vector3Field("Axis", state.m_Axis);
        state.m_Rotation = Quaternion.AngleAxis(state.m_Angle, state.m_Axis);
    }
}
