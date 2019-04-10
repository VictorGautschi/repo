using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine2D1 : MonoBehaviour
{

    [SerializeField]
    protected LineRenderer m_LineRenderer;
    [SerializeField]
    protected bool m_AddCollider = false;
    [SerializeField]
    protected EdgeCollider2D m_EdgeCollider2D;
    [SerializeField]
    protected Camera m_Camera;
    protected List<Vector2> m_Points;

    float lineDisappearTime;

    void Awake()
    {
        //m_LineRenderer = gameObject.GetComponent<LineRenderer>();
        //m_EdgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();

        //Debug.Log("Im awake");

        m_Points = new List<Vector2>();

        //m_LineRenderer.material = new Material(Shader.Find("Particles/Additive"));

        //Input.simulateMouseWithTouches = false;
    }

    private void Start()
    {
        // A simple 2 color gradient with a fixed alpha of 1.0f.
        //float alpha = 1.0f;
        //Gradient gradient = new Gradient();
        //gradient.SetKeys(
        //    new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
        //    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });
        //m_LineRenderer.colorGradient = gradient;
    }

    void Update()
    {
        //Debug.Log("In Update at least");
        // Works fine here
        if (Input.GetMouseButtonDown(0))
        {
            Reset();
           // Debug.Log("Down");
        }

        // Does not work here *******************
        //if (lineDisappearTime >= 3f){
        //    Reset();
        //    lineDisappearTime = 0f;
        //}

        //#if UNITY_IOS
        //    if (Input.touchPressureSupported)
        //    {
        //        if (Input.touchCount > 0)
        //        {
        //            // Debug.Log("In4");

        //            Vector2 touchPosition = m_Camera.ScreenToWorldPoint(Input.GetTouch(0).position);
        //            if (!m_Points.Contains(touchPosition))
        //            {
        //                //Debug.Log("In5");

        //                m_Points.Add(touchPosition);
        //                m_LineRenderer.positionCount = m_Points.Count;
        //                m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, touchPosition);
        //                if (m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1)
        //                {
        //                    m_EdgeCollider2D.points = m_Points.ToArray();
        //                    //Debug.Log("In6");

        //                    //lineDisappearTime += Time.deltaTime;
        //                }
        //            }
        //        }
        //    } else {
        //        if (Input.GetMouseButton(0))
        //        {
        //            // Debug.Log("In4");

        //            Vector2 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        //            if (!m_Points.Contains(mousePosition))
        //            {
        //                //Debug.Log("In5");

        //                m_Points.Add(mousePosition);
        //                m_LineRenderer.positionCount = m_Points.Count;
        //                m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, mousePosition);
             
        //                if (m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1)
        //                {
        //                    m_EdgeCollider2D.points = m_Points.ToArray();
        //                    //Debug.Log("In6");

        //                    //lineDisappearTime += Time.deltaTime;
        //                }
        //            }
        //        }
        //    }
        //#elif UNITY_ANDROID

        if (Input.GetMouseButton(0))
        {
            // Debug.Log("In4");

            Vector2 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            if (!m_Points.Contains(mousePosition))
            {
                //Debug.Log("In5");

                m_Points.Add(mousePosition);
                m_LineRenderer.positionCount = m_Points.Count;
                m_LineRenderer.SetPosition(m_LineRenderer.positionCount - 1, mousePosition);
                if (m_EdgeCollider2D != null && m_AddCollider && m_Points.Count > 1)
                {
                    m_EdgeCollider2D.points = m_Points.ToArray();
                    //Debug.Log("In6");

                    //lineDisappearTime += Time.deltaTime;
                }
            }
        }
        //#endif
        //lineDisappearTime += Time.deltaTime;
    }

    protected virtual void Reset()
    {
        //Debug.Log("m_LineRenderer " + m_LineRenderer);
        //Debug.Log("m_Points " + m_Points);
        //Debug.Log("m_EdgeCollider2D1 " + m_EdgeCollider2D.points[0]);
        //Debug.Log("m_EdgeCollider2D2 " + m_EdgeCollider2D.points[1]);

        if (m_LineRenderer != null)
        {
            m_LineRenderer.positionCount = 0;
        }
        if (m_Points != null)
        {
            m_Points.Clear();
        }
        if ( m_EdgeCollider2D != null && m_AddCollider)
        {
            m_EdgeCollider2D.Reset();
        }
    }

    protected virtual void CreateDefaultLineRenderer()
    {
        m_LineRenderer = gameObject.AddComponent<LineRenderer>();
        m_LineRenderer.positionCount = 0;
        m_LineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        m_LineRenderer.startColor = Color.blue;
        m_LineRenderer.endColor = Color.yellow;
        m_LineRenderer.startWidth = 0.2f;
        m_LineRenderer.endWidth = 0.2f;
        m_LineRenderer.useWorldSpace = true;
    }

    protected virtual void CreateDefaultEdgeCollider2D()
    {
        m_EdgeCollider2D = gameObject.AddComponent<EdgeCollider2D>();
    }
}