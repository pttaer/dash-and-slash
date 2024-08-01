using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MDMeleePositionView : MonoBehaviour
{
    Vector3 m_MousePos;
    Vector3 m_ScreenPoint;
    Vector3 m_Offset;
    public float m_Angle;
    public int m_NormalAttackDamage = 10;

    void Update()
    {
        m_MousePos = Input.mousePosition;
        m_ScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        m_Offset = new Vector2(m_MousePos.x - m_ScreenPoint.x, m_MousePos.y - m_ScreenPoint.y);

        m_Angle = Mathf.Atan2(m_Offset.y, m_Offset.x) * Mathf.Rad2Deg;
        Debug.Log("Run here m_Angle: " + m_Angle);

        m_Angle -= 45; // offset swing angle
        transform.localRotation = Quaternion.Euler(0, 0, m_Angle);
    }
}
