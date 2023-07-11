// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：

using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Fusion;
using TMPro;

public class NetworkPlayer : NetworkBehaviour,IPlayerLeft
{    
    
    public TextMeshPro PlayerName1;
    public TextMeshPro PlayerName2;
    public static NetworkPlayer local { get; set; }
    public SendMessages _SendMessages;

    [Networked(OnChanged = nameof(OnNickNameChanged))] public NetworkString<_16> nickName { set; get; }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            local = this;
            Debug.Log("local player");
            RPC_SetNickName(PlayerPrefs.GetString("playerNickName"));
        }
        else
        {
            // CinemachineVirtualCamera localCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            // localCamera.enabled = false;
            CinemachineFreeLook ccc = GetComponentInChildren<CinemachineFreeLook>();
            ccc.enabled = false;
            Debug.Log("Remote Player");
        }

    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }

    static void OnNickNameChanged(Changed<NetworkPlayer> changed)
    {
        changed.Behaviour.OnNickNameChanged();
    }

    private void OnNickNameChanged()
    {
        PlayerName1.text = nickName.ToString();
        PlayerName2.text = nickName.ToString();
    }
    
    [Rpc(RpcSources.InputAuthority,RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName,RpcInfo info = default)
    {
        this.nickName = nickName;
    }
}

