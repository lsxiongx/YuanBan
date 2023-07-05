using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;
/*
 依赖Navigation:在window-->AI-->Navigation中设置
 */
public class NavigationAgentScript : MonoBehaviour
{
    [Header("目标位置")]
    /*目标位置*/
    [SerializeField]
    private Vector3 m_destTarget = Vector3.zero;
    private Vector3 m_origPosition = Vector3.zero;

    private NavMeshAgent m_aiAgent;
    [Header("移动速度")]
    /*移动速度*/
    public float MoveSpeed = 7;
    [Header("与目标的距离误差")]
    /*与目标的距离误差*/
    public float errorOfDest = 0.1f;
    public NavMeshAgent GetAIAgent()
    {
        return m_aiAgent;
    }
    [Header("来回移动")]
    public bool m_moveRepeat = false;
    private void Start()
    {
        m_aiAgent = this.gameObject.GetComponent<NavMeshAgent>();
        if (m_aiAgent == null)
        {
            m_aiAgent = this.gameObject.AddComponent<NavMeshAgent>();
        }
        List<Transform> trans = new List<Transform>();
        trans.Add(this.transform);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            trans.Add(this.transform.GetChild(i));
        }

        float x = 0, y = 0, z = 0;
        // 计算模型宽高
        foreach (var child in trans)
        {
            Vector3 minV = Vector3.zero;
            Vector3 maxV = Vector3.zero;
            if (child.GetComponent<Collider>())
            {
                minV = child.GetComponent<Collider>().bounds.min;
                maxV = child.GetComponent<Collider>().bounds.max;
            }
            else if (child.GetComponent<Renderer>())
            {
                minV = child.GetComponent<Renderer>().bounds.min;
                maxV = child.GetComponent<Renderer>().bounds.max;
            }
            x = Mathf.Max(Mathf.Abs(minV.x - maxV.x), x);
            y = Mathf.Max(Mathf.Abs(minV.y - maxV.y), y);
            z = Mathf.Max(Mathf.Abs(minV.z - maxV.z), z);
        }
        //m_aiAgent.height = 1;//y;
        //m_aiAgent.radius = 1;//Mathf.Max(x,z)/2;
    }
    [Header("自定义路径")]
    public List<Vector3> path = new List<Vector3>();
    bool m_autoMoved = false;
    /*开始严指定路径移动*/
    public virtual void OnStartCustomNavigation()
    {
        if (path.Count == 0)
            return;
        path.Insert(0, transform.position);
        m_autoMoved = true;
        m_CurrentPathPointIndex = 1;
        m_PreviousPathPointIndex = 0;
        m_origPosition = transform.position;
    }
    public virtual void OnStartAINavigation()
    {
        NavMeshPath pathNiv = new NavMeshPath();

        // m_aiAgent.destination= m_destTarget; 
        m_autoMoved = m_aiAgent.CalculatePath(m_destTarget, pathNiv);
        //m_aiAgentCanMoved = NavMesh.CalculatePath(transform.position, m_destTarget, NavMesh.AllAreas, path);
        if (m_autoMoved)
        {
            m_CurrentPathPointIndex = 1;
            m_PreviousPathPointIndex = 0;
        }
        m_origPosition = transform.position;
        path.Clear();
        path = pathNiv.corners.ToList();
    }
    public virtual void OnAutoNavigation()
    {
        m_autoMoved = false;
        path.Clear();
        m_CurrentPathPointIndex = 0;
        m_PreviousPathPointIndex = 0;
    }
    int m_CurrentPathPointIndex = 0;
    private int m_PreviousPathPointIndex = 0;
    public virtual void OnAiNaviMove()
    {
        if (m_CurrentPathPointIndex > path.Count - 1)
        {
            return;
        }

        if ((transform.position - path[m_CurrentPathPointIndex]).magnitude <= errorOfDest)
        {
            //递增路径点索引
            m_PreviousPathPointIndex++;
            m_CurrentPathPointIndex++;
            //防止数组越界
            if (m_CurrentPathPointIndex > path.Count - 1)
            {
                //处理动画切换，请无视
                // Entity.GetComponent<StackFsmComponent>().AddState(StateTypes.Idle, "Idle", 1);
                return;
            }
            //处理人物转向，请无视
            //Entity.GetComponent<TurnComponent>().Turn(m_NavMeshPath.corners[m_CurrentPathPointIndex]);
        }

        /*移动*/
        transform.Translate(
           ((-transform.position +
             path[m_CurrentPathPointIndex]).normalized) *
           (Time.deltaTime * MoveSpeed), Space.World);
    }
    public void Update()
    {
        if (m_autoMoved)
        {
            if (m_moveRepeat)
            {
                if (m_CurrentPathPointIndex > path.Count - 1)
                {
                    OnAutoNavigation();
                    m_destTarget = m_origPosition;
                    OnStartAINavigation();
                }
            }
            OnAiNaviMove();
        }
    }
}
