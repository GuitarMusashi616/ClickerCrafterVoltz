using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NewMouseCursor : MonoBehaviour
{

	public Slot _floatingSlot;
	ItemStack _itemSlot;

	public void AddToSlot(ItemStack itemStack)
	{
		Assert.IsNotNull(itemStack, $"cannot add null itemStack to {this}");

		if (_itemSlot?.Item.name == itemStack?.Item.name)
		{
			// adding to slot with same name item
			_itemSlot += itemStack;
			OnItemAdded();
		}
		else if (IsEmpty)
		{
			// slot must be empty
			_itemSlot = itemStack;
			OnItemAdded();
		}
	}

	public ItemStack RemoveFromSlot(int maxAmount = int.MaxValue)
	{
		ItemStack itemStack = _itemSlot;
		Assert.IsNotNull(itemStack, $"Cannot remove from empty hand ({this})");
		if (_itemSlot.Count > maxAmount)
		{
			_itemSlot = itemStack - maxAmount;
			OnItemRemoved();
			return itemStack - _itemSlot.Count;
		}

		_itemSlot = null;
		OnItemRemoved();
		return itemStack;
	}

	//public ItemStack GetItem()
	//{
	//	return ;
	//}

	//public void SetItem(ItemStack itemStack)
	//{
	//	AddToSlot(itemStack);
	//}

	//public void ClearItem()
	//{
	//	RemoveFromSlot();
	//}

	public ItemStack ItemSlot
	{
		get => _itemSlot;
	}

	public bool IsEmpty
	{
		get => _itemSlot == null;
	}

	void OnItemAdded()
	{
		Assert.IsNotNull(_itemSlot, "Item slot should not be null after adding to it");
		_floatingSlot.SetSlot(_itemSlot.Item.icon, _itemSlot.Count);
	}

	void OnItemRemoved()
	{
		if (_itemSlot == null)
		{
			_floatingSlot.ClearSlot();
			return;
		}
		_floatingSlot.SetSlot(_itemSlot.Item.icon, _itemSlot.Count);
	}

	void SetFloatingSlotToCursor()
	{
		var rt = (RectTransform)_floatingSlot.transform;
		var vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y + (rt.rect.height / 2));
		_floatingSlot.transform.position = vec;
	}


	void Start()
    {
		Assert.IsNotNull(_floatingSlot, $"floatingSlot in {this} cannot be null");
	}

    void Update()
    {
		if (!IsEmpty)
		{
			SetFloatingSlotToCursor();
		}
	}
}
