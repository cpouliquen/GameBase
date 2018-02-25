using UnityEditor;

[CustomEditor(typeof(Tweener))]
public class TweenerEditor : Editor
{
    private Tweener m_Target;

    void OnEnable()
    {
        m_Target = (Tweener)target;
    }

    public override void OnInspectorGUI()
    {
        DrawEditor(m_Target);
    }

    public static void DrawEditor(Tweener _Target)
    {
        _Target.m_IsUI = EditorGUILayout.Toggle("Is UI", _Target.m_IsUI);
        _Target.m_PlayAtStart = EditorGUILayout.Toggle("Play at start", _Target.m_PlayAtStart);
        _Target.m_Loop = EditorGUILayout.Toggle("Loop", _Target.m_Loop);
        _Target.m_PingPong = EditorGUILayout.Toggle("Ping pong", _Target.m_PingPong);
        _Target.m_OverrideStartState = EditorGUILayout.Toggle("Override start state", _Target.m_OverrideStartState);
        _Target.m_HasDelay = EditorGUILayout.Toggle("Has delay", _Target.m_HasDelay);

        if (_Target.m_HasDelay)
        {
            _Target.m_RepeatableDelay = EditorGUILayout.Toggle("Repeatable delay", _Target.m_RepeatableDelay);
            _Target.m_Delay = EditorGUILayout.FloatField("Delay", _Target.m_Delay);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
    }
}
