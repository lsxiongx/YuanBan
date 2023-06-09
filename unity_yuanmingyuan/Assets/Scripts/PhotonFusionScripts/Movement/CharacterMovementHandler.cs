// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：

using System;
using UnityEngine;
using Fusion;
using Fusion.Editor;

public class CharacterMovementHandler : NetworkBehaviour
{
    private NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    
    /// <summary>
    /// 移动方向
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// 平滑旋转时间
    /// </summary>
    public float turnSmoothTime = 0.1f;

    /// <summary>
    /// 平滑旋转速度
    /// </summary>
    private float turnSmoothVelocity;

    private Animator PlayerAnim;

    [Networked] public NetworkString<_16> AnimStr { set; get; }

    private float moveSpeed = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        PlayerAnim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        RPC_SetNickAnim("null");
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out  NetWorkInputData netWorkInputData))
        {
            direction = new Vector3(netWorkInputData.Movements.x, 0f, netWorkInputData.Movements.y);
            moveSpeed = direction.normalized.magnitude;
            if (direction.magnitude >= 0.01f)
            {
                float targetangle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + netWorkInputData.rotationInput;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f,angle,0f);
                Vector3 MoveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
                networkCharacterControllerPrototypeCustom.Move(MoveDir);
            }
            if (AnimStr != "null")
            {
                Debug.Log("Button___" + AnimStr);
                SwitchAnimator(AnimStr.ToString());
            }
            speedAnimation();
        }
    }

    [Rpc(RpcSources.InputAuthority,RpcTargets.All)]
    private void RPC_SetNickAnim(string nickName,RpcInfo info = default)
    {
        Debug.Log("AnimStr is: " + nickName);
        this.AnimStr = nickName;
        Debug.Log("------------------------------" + AnimStr);
    }
    
    public void setAnimStr(string str)
    {
        RPC_SetNickAnim(str);
    }
    private void speedAnimation()
    {
        //animator.SetFloat("Speed", PlayerRigidbody.velocity.sqrMagnitude);
        PlayerAnim.SetFloat("Speed",moveSpeed);
    }
    
    public void SwitchAnimator(string animatorId)
    {
        PlayerAnim.SetTrigger(animatorId);
        RPC_SetNickAnim("null");
    }
}
