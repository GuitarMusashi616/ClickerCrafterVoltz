using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, ISwappable
{
	public Image icon;
	public Text text;
	ItemStack itemStack;
	MouseCursor mouse;

	public ItemStack GetItem()
	{
		return itemStack;
	}

	public void SetItem(ItemStack newItem)
	{
		itemStack = newItem;
		icon.sprite = newItem.Item.icon;
		icon.enabled = true;
		icon.color = new Color(1, 1, 1, 1);
		text.text = newItem.Count.ToString();
	}

	public void SwapItems()
	{
		if (!IsEmpty && mouse.IsEmpty)
		{
			mouse.SetItem(itemStack);
			ClearItem();
			// print($"now itemStack shows {IsEmpty}");
		}
		else if (!IsEmpty && !mouse.IsEmpty)
		{
			SetItem(mouse.GetItem());
			mouse.ClearItem();
		}
		//else if (!IsEmpty && !mouse.IsEmpty)
		//{
		//	ItemStack temp = mouse.GetItem();
		//	mouse.ClearItem();
		//	mouse.SetItem(itemStack);
		//	ClearItem();
		//	SetItem(temp);
		//}

	}

	public ItemStack DropItem()
	{
		ItemStack dropItem = itemStack.Clone() as ItemStack;
		return dropItem;
	}

	public void ClearItem()
	{
		print("clear item has been called");
		itemStack = null;
		icon.sprite = null;
		icon.enabled = false;
		text.text = string.Empty;
	}

	public bool IsEmpty
	{
		get => itemStack == null;
	}

    // Start is called before the first frame update
    void Start()
    {
		mouse = FindObjectOfType<MouseCursor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnPointerDown(PointerEventData eventData)
	{
		// case both
		// case slot empty but hand full
		// case hand empty but slot full
		// case hand empty and slot empty
		print(IsEmpty);
		SwapItems();

	}

}
