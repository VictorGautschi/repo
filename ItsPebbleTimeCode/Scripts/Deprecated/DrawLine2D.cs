using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine2D : MonoBehaviour
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

    public virtual LineRenderer LineRenderer
    {
        get
        {
            return m_LineRenderer;
        }
    }

    public virtual bool AddCollider
    {
        get
        {
            return m_AddCollider;
        }
    }

    public virtual EdgeCollider2D EdgeCollider2D
    {
        get
        {
            return m_EdgeCollider2D;
        }
    }

    public virtual List<Vector2> Points
    {
        get
        {
            return m_Points;
        }
    }

    private void Awake()
    {

        Debug.Log("Im awake");
        if (m_LineRenderer == null)
        {
            Debug.LogWarning("DrawLine: Line Renderer not assigned, Adding and Using default Line Renderer.");
            CreateDefaultLineRenderer();
        }
        if (m_EdgeCollider2D == null && m_AddCollider)
        {
            Debug.LogWarning("DrawLine: Edge Collider 2D not assigned, Adding and Using default Edge Collider 2D.");
            CreateDefaultEdgeCollider2D();
        }
        if (m_Camera == null)
        {
            m_Camera = Camera.main;
        }
        m_Points = new List<Vector2>();

        //Input.simulateMouseWithTouches = false;
    }

    private void Start()
    {
        Debug.Log("Im start");

    }

    void Update()
    {

        //Debug.Log("In Update at least");
        // Works fine here
        if (Input.GetMouseButtonDown(0))
        {
            Reset();
            Debug.Log("Down");
        }

        // Does not work here *******************
        //if (lineDisappearTime >= 3f){
        //    Reset();
        //    lineDisappearTime = 0f;
        //}

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