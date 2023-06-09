// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：

using UnityEngine;
using Fusion;

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

    private float moveSpeed = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        PlayerAnim = GetComponentInChildren<Animator>();
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
            speedAnimation();
        }
    }
    
    private void speedAnimation()
    {
        //animator.SetFloat("Speed", PlayerRigidbody.velocity.sqrMagnitude);
        PlayerAnim.SetFloat("Speed",moveSpeed);
    }
    public void SwitchAnimator(string animatorId)
    {
        switch (animatorId)
        {
            case "dance":
                PlayerAnim.SetTrigger("dance");
                break;
            case "seat":
                PlayerAnim.SetTrigger("seat");
                break;
            case "jump":
                PlayerAnim.SetTrigger("jump");
                break;
            case "wave":
                PlayerAnim.SetTrigger("wave");
                break;
        }
    }
}
