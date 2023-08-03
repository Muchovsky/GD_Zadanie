using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static DataItem;

public class Item : MonoBehaviour
{
    [SerializeField] Image badgeImage;
    [SerializeField] Image glowImage;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI number;

    [Inject] ItemSpriteList spriteList;

    public void Init(int number, CategoryType type, string descriptuon, bool isSpecial)
    {
        this.number.text = number.ToString();
        badgeImage.sprite = spriteList.GetByType(type).Sprite;
        description.text = descriptuon;
        glowImage.gameObject.SetActive(isSpecial);
    }

    public void Init(int number, DataItem dataitem)
    {
        this.number.text = number.ToString();
        badgeImage.sprite = spriteList.GetByType(dataitem.Category).Sprite;
        description.text = dataitem.Description;
        glowImage.gameObject.SetActive(dataitem.Special);
    }
}