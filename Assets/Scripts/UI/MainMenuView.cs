using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : SingletonMB<MainMenuView>
{
    private const float     c_TransitionDuration = 0.3f;

    public CanvasGroup      m_Group;
    public Text             m_BestScore;

    private Coroutine       m_TransitionCoroutine;
    private GameManager     m_GameManager;
    private SaveManager     m_SaveManager;

    void Awake()
    {
        m_GameManager = GameManager.Instance;
        m_SaveManager = SaveManager.Instance;
        m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
    }

    public void OnPlayButton()
    {
        if (m_GameManager.currentPhase == GamePhase.MAIN_MENU)
            m_GameManager.ChangePhase(GamePhase.GAME);
    }

    private void OnGamePhaseChanged(GamePhase _GamePhase)
    {
        if (m_TransitionCoroutine != null)
            StopCoroutine(m_TransitionCoroutine);

        if (_GamePhase == GamePhase.MAIN_MENU)
        {
            m_BestScore.text = m_SaveManager.GetBestScore().ToString();
            m_TransitionCoroutine = StartCoroutine(Transition(true));
        }
        else if (_GamePhase == GamePhase.END)
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
}
