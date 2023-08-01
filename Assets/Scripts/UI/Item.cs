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

    [Inject] ItemSpriteList spriteList;
    public void Init(CategoryType type, string descriptuon, bool isSpecial)
    {
        badgeImage.sprite = spriteList.GetByType(type).Sprite;
        description.text = descriptuon;
        glowImage.gameObject.SetActive(isSpecial);
    }
}
