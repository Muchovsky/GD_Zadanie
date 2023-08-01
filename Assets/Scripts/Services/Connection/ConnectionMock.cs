using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ConnectionMock : MonoBehaviour
{
    CancellationTokenSource cancellationTokenSource;
    [Inject]
    DataServerMock dataServerMock;
    [Inject]
    PrefabManager prefabManager;

    public async Task<int> InitializeAsync()
    {
        var loadingScreen = prefabManager.GetPrefab<LoadingUI>(PrefabNameEnum.LOADINGSCREENUI, null);
        loadingScreen.Show();
        var x = await RequestData();
        loadingScreen.Hide();
        Debug.Log(x);
        return x;
    }
    async Task<int> RequestData()
    {
        cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = cancellationTokenSource.Token;
        try
        {
            var task = dataServerMock.DataAvailable(token);
            int result = await task;
            return result;
        }
        catch
        {
            Debug.Log("Task was cancelled!");
            return -1;
        }
        finally
        {
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }


}