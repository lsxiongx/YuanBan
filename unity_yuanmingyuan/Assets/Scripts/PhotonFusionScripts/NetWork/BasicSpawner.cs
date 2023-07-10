// 文件名称：BasicSpawner.cs
// 功能描述：用来执行Runner的回调
// 编写作者：雄
// 编写日期：
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using LitJson;
using UnityEngine.SceneManagement;

public class BasicSpawner : MonoBehaviour,INetworkRunnerCallbacks
{
    private CharacterSelect _characterSelect;
    private CharacterInputHandler _characterInputHandler;

    private Dictionary<PlayerRef, string> PlayerDic = new Dictionary<PlayerRef, string>();
    //
    // /// <summary>
    // /// 调用前端方法，通知角色加入
    // /// </summary>
    // /// <param name="data"></param>
    // [DllImport("__Internal")]
    // private static extern void GetPlayerJoinedMessage(string data);
    //
    // /// <summary>
    // /// 调用前端方法，通知角色退出
    // /// </summary>
    // /// <param name="data"></param>
    // [DllImport("__Internal")]
    // private static extern void GetPlayerLeavedMessage(string data);
    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag("CharacterSelect");
        _characterSelect = obj.GetComponent<CharacterSelect>();
    }
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {
        if (runner.IsServer)
        {
            initDataClass GenderData = JsonMapper.ToObject<initDataClass>(PlayerPrefs.GetString("playerData"));
            PlayerPrefs.SetString("playerNickName", GenderData.playerID);
            if (string.Equals(GenderData.message.type, "female")) 
            {
                runner.Spawn(_characterSelect.femalecharacters[int.Parse(GenderData.message.model)], _characterSelect.originPos, Quaternion.identity, player);
            }
            else
            {
                runner.Spawn(_characterSelect.malecharacters[int.Parse(GenderData.message.model)], _characterSelect.originPos, Quaternion.identity, player);
            }
            PlayerDic.Add(player,GenderData.playerID);
            playRoomData playdata = new playRoomData();
            playdata.playId = PlayerDic[player];
            playdata.roomNo = runner.SessionInfo.Name;
            playdata.message = "string";
            string JsonMessage = JsonMapper.ToJson(playdata);
            Debug.Log("当前列表玩家数：" + PlayerDic.Count);
            Debug.Log("加入的玩家信息为：" + JsonMessage);
            // if (Application.platform == RuntimePlatform.WebGLPlayer)
            // {
            //     GetPlayerJoinedMessage(JsonMessage);
            // }

        }
        else Debug.Log("Client Start Player");
        
        Debug.Log(runner.ActivePlayers.Count());
        //playerList.Add(player , networkPlaeyrobject);
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        playRoomData playdata = new playRoomData();
        playdata.playId = PlayerDic[player];
        playdata.roomNo = runner.SessionInfo.Name;
        playdata.message = "string";
        string JsonMessage = JsonMapper.ToJson(playdata);
        Debug.Log("离开的玩家信息为：" + JsonMessage);
        // if (Application.platform == RuntimePlatform.WebGLPlayer)
        // {
        //     GetPlayerLeavedMessage(JsonMessage);
        // }
        PlayerDic.Remove(player);
    }
    public void OnInput(NetworkRunner runner, NetworkInput input) {
        if (_characterInputHandler == null && NetworkPlayer.local != null)
        {
            _characterInputHandler = NetworkPlayer.local.GetComponent<CharacterInputHandler>();
        }

        if (_characterInputHandler != null)
        {
            //input.Set(_characterInputHandler.GetInputValues());
            input.Set(_characterInputHandler.GetNetworkInput());
        }
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) {Debug.Log("OnConnectedToServer"); }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

}

public class playRoomData
{
    public string playId;
    public string roomNo;
    public string message;
}