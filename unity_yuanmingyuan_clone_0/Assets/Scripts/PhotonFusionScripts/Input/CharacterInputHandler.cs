// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：

using System.Runtime.InteropServices;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;

    private Transform LocalCamera;
    public VirtualJoystick virtualJoystick;

    [DllImport("__Internal")]
    private static extern bool HelloPlatform();
    private void Awake()
    {
        virtualJoystick = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).GetComponent<VirtualJoystick>();
        LocalCamera = Camera.main.transform;
    }

    public void setInputMove(float x,float y)
    {
        moveInputVector.x = x;
        moveInputVector.y = y;
    }
    void LateUpdate()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (!HelloPlatform())
            {
                //Mobile Move
                setInputMove(virtualJoystick.GetDir().x, virtualJoystick.GetDir().y);
            }
            else
            {
                //PcMove
                setInputMove(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
            }
        }
        else
        {
            //setInputMove(virtualJoystick.GetDir().x, virtualJoystick.GetDir().y);

            //PcMove
            setInputMove(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        }
        
        // moveInputVector.x = Input.GetAxis("Horizontal");
        // moveInputVector.y = Input.GetAxis("Vertical");
    }

    public NetWorkInputData GetNetworkInput()
    {
        NetWorkInputData netWorkInputData = new NetWorkInputData();

        //Aim data
        //netWorkInputData.InputRotation = LocalCamera.transform.forward;
        netWorkInputData.rotationInput = LocalCamera.transform.eulerAngles.y;
        netWorkInputData.Movements = moveInputVector;
        return netWorkInputData;
    }
}