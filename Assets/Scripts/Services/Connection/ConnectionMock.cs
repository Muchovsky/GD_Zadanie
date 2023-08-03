using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ConnectionMock
{
    CancellationTokenSource cancellationTokenSource;

    [Inject] DataServerMock dataServerMock;

    public async Task<int> RequestNumberOfItems()
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
            Debug.Log("Can't connect to server!");
            return -1;
        }
        finally
        {
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }

    public async Task<IList<DataItem>> RequestItems(int index, int count)
    {
        cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = cancellationTokenSource.Token;
        try
        {
            var task = dataServerMock.RequestData(index, count, token);
            var result = await task;
            return result;
        }
        catch
        {
            Debug.Log("Can't recive data from server!");
            return null;
        }
        finally
        {
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }
}