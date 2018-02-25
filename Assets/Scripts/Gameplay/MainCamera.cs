using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : SingletonMB<MainCamera>
{
    private Transform   m_Transform;
    private Transform   m_Target;
    private Vector3     m_PosBuffer;

    void Awake()
    {
        m_Transform = transform;
        m_PosBuffer = m_Transform.position;

        // TODO : set target
    }

    void Update()
    {
        if (m_Target == null)
            return;

        m_PosBuffer.Set(m_Target.position.x, m_Target.position.y, m_PosBuffer.z);
        m_Transform.position = m_PosBuffer;
    }
}
