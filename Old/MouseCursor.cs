using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MouseCursor : MonoBehaviour
{

	public Slot _floatingSlot;
	ItemStack _itemStack;

	public event EventHandler<EventArgs> ItemAdded;
	public event EventHandler<EventArgs> ItemRemoved;


	public void SetItem(ItemStack itemStack)
	{
		_itemStack = itemStack;
		_floatingSlot.SetSlot(_itemStack.Item.icon, _itemStack.Count);
	}

	public ItemStack GetItem()
	{
		return _itemStack;
	}

	public void ClearItem()
	{
		_itemStack = null;
		_floatingSlot.ClearSlot();
	}

	public ItemStack RemoveItem(int maxAmount = int.MaxValue)
	{
		//Assert.IsNotNull(_itemStack, $"Cannot remove from empty hand {this}");

		//// take up to maxAmount
		//int amountToTake = maxAmount < _itemStack.Count ? maxAmount : _itemStack.Count;
		//ItemStack resultForMouse = _itemStack - amountToTake;
		//_itemStack = resultForMouse;
		//OnItemRemoved();


		ItemStack itemStack = _itemStack;
		Assert.IsNotNull(_itemStack, $"Cannot remove from empty hand {this}");
		if (_itemStack.Count > maxAmount)
		{
			_itemStack = itemStack - maxAmount;
			OnItemRemoved();
			return itemStack - _itemStack.Count;
		}

		_itemStack = null;
		OnItemRemoved();
		return itemStack;
	}

	//public ItemStack DropItem()
	//{
	//	// drops one from item stack
	//	if (_itemStack != null)
	//	{
	//		if (_itemStack.Count > 1)
	//		{
	//			SetItem(_itemStack - 1);
	//			return new ItemStack(_itemStack.Item, 1);
	//		}
	//		else
	//		{
	//			var temp = _itemStack;
	//			ClearItem();
	//			return temp;
	//		}
	//	}
	//	return null;
	//}

	public bool IsEmpty
	{
		get => _itemStack == null;
	}

	void Start()
	{
		Assert.IsNotNull(_floatingSlot, $"floatingSlot in {this} cannot be null");
	}

	void OnItemAdded()
	{
		ItemAdded?.Invoke(this, EventArgs.Empty);
	}

	void OnItemRemoved()
	{
		ItemRemoved?.Invoke(this, EventArgs.Empty);
	}

	void SetFloatingSlotToCursor()
	{
		var rt = (RectTransform)_floatingSlot.transform;
		var vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y + (rt.rect.height / 2));
		_floatingSlot.transform.position = vec;
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
