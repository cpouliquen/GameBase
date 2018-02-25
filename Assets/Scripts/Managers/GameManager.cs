using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    MAIN_MENU,
    GAME,
    END
}

public class GameManager : SingletonMB<GameManager>
{
    public delegate void OnGamePhaseChanged(GamePhase _GamePhase);
    public event OnGamePhaseChanged onGamePhaseChanged;

    private GamePhase       m_CurrentPhase;
    public GamePhase        currentPhase { get { return m_CurrentPhase; } }

    private int             m_Score;

    private SaveManager     m_SaveManager;
    private ScoreView       m_ScoreView;

    void Awake()
    {
        m_SaveManager = SaveManager.Instance;
        m_ScoreView = ScoreView.Instance;
    }

	public void ChangePhase(GamePhase _GamePhase)
    {
        switch (_GamePhase)
        {
            case GamePhase.MAIN_MENU:
                break;

            case GamePhase.GAME:
                m_Score = 0;
                m_ScoreView.SetScore(m_Score);
                break;

            case GamePhase.END:
                m_SaveManager.ReplaceBestIfNeeded(m_Score);
                break;
        }

        m_CurrentPhase = _GamePhase;

        if (onGamePhaseChanged != null)
            onGamePhaseChanged.Invoke(_GamePhase);
    }

    public void AddScore(int _Value)
    {
        m_Score += _Value;
        m_ScoreView.SetScore(m_Score);
    }
}
