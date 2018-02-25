using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : SingletonMB<ScoreView>
{
    private const float     c_TransitionDuration = 0.3f;

    public CanvasGroup      m_Group;
    public Text             m_Score;

    private Coroutine       m_TransitionCoroutine;
    private GameManager     m_GameManager;

    void Awake()
    {
        m_GameManager = GameManager.Instance;
        m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
    }

    private void OnGamePhaseChanged(GamePhase _GamePhase)
    {
        if (m_TransitionCoroutine != null)
            StopCoroutine(m_TransitionCoroutine);

        if (_GamePhase == GamePhase.GAME)
            m_TransitionCoroutine = StartCoroutine(Transition(true));
        else
            m_TransitionCoroutine = StartCoroutine(Transition(false));
    }

    private IEnumerator Transition(bool _Appear)
    {
        m_Group.interactable = false;

        float time = 0.0f;
        while (time < 1.0f)
        {
            time += Time.deltaTime / c_TransitionDuration;
            m_Group.alpha = _Appear ? time : (1.0f - time);
            yield return null;
        }

        if (_Appear)
            m_Group.interactable = true;
    }

    public void SetScore(int _Score)
    {
        m_Score.text = _Score.ToString();
    }
}
