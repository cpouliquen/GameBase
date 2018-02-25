using UnityEngine;

public class Tweener : MonoBehaviour
{
    public bool                 m_IsUI = false;
    public bool                 m_PlayAtStart = false;
    public bool                 m_Loop = false;
    public bool                 m_PingPong = false;
    public bool                 m_OverrideStartState = false;
    public bool                 m_HasDelay;
    public bool                 m_RepeatableDelay = false;
    public float                m_Delay;

    protected int               m_CurrentStateId = 0;
    protected int               m_NextStateId = 1;
    protected bool              m_IsDelayed = false;
    private bool                m_StartToEnd = true;

    protected bool              m_IsPlaying = false;
    protected float             m_Time;

    // Cached components
    protected RectTransform     m_UITransform;
    protected Transform         m_Transform;

    void Awake()
    {
        AwakeSpecific();
    }

    protected virtual void AwakeSpecific()
    {
    }

    void OnEnable()
    {
        if (m_PlayAtStart)
            Play();
    }

    void OnDisable()
    {
        Stop();
    }

    public virtual void Play()
    {
        m_IsPlaying = true;
        m_Time = 0.0f;
        m_StartToEnd = true;
        m_CurrentStateId = 0;
        m_NextStateId = 1;

        if (m_HasDelay)
            m_IsDelayed = true;
    }

    public virtual void Stop()
    {
        m_IsPlaying = false;
        m_Time = 0.0f;
        m_StartToEnd = true;
        m_CurrentStateId = 0;
        m_NextStateId = 1;
    }

    void Update()
    {
        if (m_IsPlaying)
        {
            float dt = m_IsUI ? Time.unscaledDeltaTime : Time.deltaTime;

            if (m_IsDelayed)
            {
                if (m_Time < 1.0f)
                    m_Time += dt / m_Delay;
                else
                {
                    m_IsDelayed = false;
                    m_Time = 0.0f;
                }

                return;
            }

            UpdateSpecific(dt);
        } 
    }

    protected virtual void UpdateSpecific(float _Dt) {}

    protected virtual int GetStateCount()
    {
        return 0;
    }

    protected virtual void GoToNextState()
    {
        m_Time = 0.0f;

        if (m_PingPong)
        {
            if (m_StartToEnd)
            {
                m_CurrentStateId++;
                m_NextStateId++;

                if (m_NextStateId >= GetStateCount())
                {
                    if (m_Loop)
                    {
                        if (m_HasDelay)
                            m_IsDelayed = true;

                        m_StartToEnd = false;
                        m_CurrentStateId = GetStateCount() - 1;
                        m_NextStateId = m_CurrentStateId - 1;
                    }
                    else
                        Stop();
                }
            }
            else
            {
                m_CurrentStateId--;
                m_NextStateId--;

                if (m_NextStateId < 0)
                {
                    if (m_Loop)
                    {
                        m_StartToEnd = true;
                        m_CurrentStateId = 0;
                        m_NextStateId = 1;
                    }
                    else
                        Stop();
                }
            }
        }
        else
        {
            m_CurrentStateId++;
            m_NextStateId++;

            if (m_NextStateId >= GetStateCount())
            {
                if (m_Loop)
                {
                    if (m_HasDelay)
                        m_IsDelayed = true;

                    m_CurrentStateId = 0;
                    m_NextStateId = 1;
                }
                else
                    Stop();
            }
        }
    }


}
