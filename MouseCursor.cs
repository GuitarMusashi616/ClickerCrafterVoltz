using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MouseCursor : MonoBehaviour
{

	public Slot floatingSlot;

	ItemStack _itemStack;


	public void SetItem(ItemStack itemStack)
	{
		_itemStack = itemStack;
		floatingSlot.SetSlot(_itemStack.Item.icon, _itemStack.Count);
	}

	public ItemStack GetItem()
	{
		return _itemStack;
	}

	public void ClearItem()
	{
		_itemStack = null;
		floatingSlot.ClearSlot();
	}

	public bool IsEmpty
	{
		get => _itemStack == null;
	}


	void Start()
	{
		Assert.IsNotNull(floatingSlot, $"floatingSlot in {this} cannot be null");
	}

	void SetFloatingSlotToCursor()
	{
		var rt = (RectTransform)floatingSlot.transform;
		var vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y + (rt.rect.height / 2));
		floatingSlot.transform.position = vec;
	}

	// Update is called once per frame
	void Update()
	{
		if (!IsEmpty)
		{
			SetFloatingSlotToCursor();
		}
	}

}
