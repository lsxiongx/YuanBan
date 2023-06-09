// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：

using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour,IPlayerLeft
{
    public static NetworkPlayer local { get; set; }

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
        Debug.Log($"{Time.time} OnHPChanged value {changed.Behaviour.nickName}");

        changed.Behaviour.OnNickNameChanged();
    }

    private void OnNickNameChanged()
    {
        Debug.Log($"Nickname changed for player to {nickName} for player {gameObject.name}");

    }
    
    [Rpc(RpcSources.InputAuthority,RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName,RpcInfo info = default)
    {
        this.nickName = nickName;
    }
}

