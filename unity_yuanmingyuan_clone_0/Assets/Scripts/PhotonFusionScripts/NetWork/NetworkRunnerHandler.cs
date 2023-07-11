// 文件名称：***.cs
// 功能描述：
// 编写作者：雄
// 编写日期：
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using System.Linq;


public class NetworkRunnerHandler : MonoBehaviour
{
    public NetworkRunner NetworkRunnerPrefab;
    private NetworkRunner networkRunner;
    private GameMode moshi;

    // private void Start()
    // {
    //     StartGames();
    // }

    public void StartGames()
    {
        networkRunner = Instantiate(NetworkRunnerPrefab);
        networkRunner.name = "Network Runner";
        
        moshi = GameMode.Server;
#if UNITY_EDITOR
        moshi = GameMode.AutoHostOrClient;
#elif UNITY_WEBGL
        moshi = GameMode.AutoHostOrClient;
#endif
        var ClientTask =  StartNetworkRunner(networkRunner,moshi , NetAddress.Any(), SceneManager.GetActiveScene().buildIndex,null);
        Debug.Log("Start Games");
        Debug.Log(moshi);
    }

    protected virtual Task StartNetworkRunner(NetworkRunner runner,  GameMode gameMode ,NetAddress netAddress , SceneRef scene , Action<NetworkRunner> initialized)
    {
        var sceneManager = networkRunner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>()
            .FirstOrDefault();
        if (sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Address = netAddress,
            Scene = scene,
            SessionName = "First Room",
            Initialized = initialized,
            SceneManager = sceneManager
        });
    }
}