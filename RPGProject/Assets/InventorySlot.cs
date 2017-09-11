using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {


    private Item _item;
    public Image icon;
    public Button removeButton;

    public void AddItem(Item item)
    {
        _item = item;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        removeButton.image.enabled = true;
    }

    public void Clear()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        removeButton.image.enabled = false;
    }

    public void OnRemoveButtonClick()
    {
        Inventory.instance.Remove(_item);
    }

    public void UseItem()
    {
        if (_item == null)
            return;

        _item.Use();
    }
}
