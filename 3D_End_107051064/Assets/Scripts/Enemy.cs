using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent nav;
    private Animator ani;

    [Header("移動速度"), Range(0, 50)]
    public float speed = 3;
    [Header("停止的距離"), Range(0, 50)]
    public float StopDistance = 1.5f;
    [Header("攻擊冷卻"), Range(0, 50)]
    public float cd = 2f;
    [Header("攻擊中心點"), ]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atkLength;

    /// <summary>
    /// 繪製圖示事件：僅在Uniny 內顯示
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atkLength);
    }

    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;
    /// <summary>
    /// 射線擊中的物件
    /// </summary>
    private RaycastHit hit;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Sapphiart").transform;

        nav.speed = speed;
        nav.stoppingDistance = StopDistance;
    }
    private void Update()
    {
        Track();
        Attack();
    }
    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        if(nav.remainingDistance < StopDistance)
        {
            timer += Time.deltaTime;

            Vector3 pos = player.position;
            pos.y = transform.position.y;

            transform.LookAt(pos);
            if(timer >=cd)     
            {
                ani.SetTrigger("攻擊觸發");
                timer = 0;

                //物理.碰撞射線(攻擊中心點的座標，攻擊中心點的前方，攻擊長度，圖層)
                //圖層:1<<圖層編號
                //如果 射線 打到物件就執行{}
               if( Physics.Raycast(atkPoint.position, atkPoint.forward ,out hit, atkLength, 1 << 8))
                {
                    hit.collider.GetComponent<Player>().Damage();
                }
            }
            
        }
    }


    /// <summary>
    /// 追蹤
    /// </summary>
    private void Track()
    {
        nav.SetDestination(player.position);
        ani.SetBool("走路開關", nav.remainingDistance > StopDistance);
    }
}

