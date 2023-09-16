using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using static DataItem;

public class Item : MonoBehaviour
{
    [SerializeField] Image badgeImage;
    [SerializeField] Image glowImage;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI number;
    AsyncOperationHandle<Sprite> opHandle;

    public async Task Init(int number, CategoryType type, string descriptuon, bool isSpecial)
    {
        this.number.text = number.ToString();
        badgeImage.sprite = await LoadSpriteAsync(type.ToString());
        description.text = descriptuon;
        glowImage.gameObject.SetActive(isSpecial);
    }

    public async Task Init(int number, DataItem dataitem)
    {
        this.number.text = number.ToString();
        badgeImage.sprite = await LoadSpriteAsync(dataitem.Category.ToString());
        description.text = dataitem.Description;
        glowImage.gameObject.SetActive(dataitem.Special);
    }

    private async Task<Sprite> LoadSpriteAsync(string name)
    {
        opHandle = Addressables.LoadAssetAsync<Sprite>(name);
        await opHandle.Task;
        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Sprite sprite = opHandle.Result;
            return sprite;
        }
        else
        {
            Debug.LogError("Load failed");
            return null;
        }
    }
}