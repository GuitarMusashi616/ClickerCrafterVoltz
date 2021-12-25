using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NewInventoryModel : MonoBehaviour
{
	public event EventHandler<ItemStack> ItemAdded;
	public event EventHandler<ItemStack> ItemRemoved;

	SortedList<string, ItemStack> _itemSlots = new SortedList<string, ItemStack>();

	public void Add(ItemStack itemStack)
	{
		Assert.IsNotNull(itemStack, $"Cannot add null itemStack to {this}");
		string itemName = itemStack.Item.name;

		if (_itemSlots.ContainsKey(itemName))
		{
			_itemSlots[itemName] += itemStack;
			OnItemAdded(itemStack);
			return;
		}
		_itemSlots[itemName] = itemStack;
		OnItemAdded(itemStack);
	}

	public ItemStack Remove(string key, int maxAmount=int.MaxValue)
	{
		Assert.IsTrue(_itemSlots.ContainsKey(key), $"{key} not in {this}");
		ItemStack itemStack = _itemSlots[key];

		if (itemStack.Count > maxAmount)
		{
			_itemSlots[key] = itemStack - maxAmount;
			ItemStack returnItem = itemStack - _itemSlots[key].Count;
			OnItemRemoved(returnItem);
			return returnItem;
		}

		_itemSlots.Remove(key);
		OnItemRemoved(itemStack);
		return itemStack;
	}

	public ItemStack Remove(int slot, int maxAmount=int.MaxValue)
	{
		Assert.IsTrue(slot < _itemSlots.Values.Count, $"{slot} is out of range of {_itemSlots.Values.Count} for {this}");
		string key = _itemSlots.Keys[slot];
		return Remove(key, maxAmount);
	}

	public bool IsEmpty(int slot)
	{
		return slot >= _itemSlots.Count;
	}

	public ItemStack this[int slot]
	{
		get => _itemSlots.Values[slot];
	}

	public int Count { get => _itemSlots.Count; }

	public IEnumerator<ItemStack> GetEnumerator()
	{
		return _itemSlots.Values.GetEnumerator();
	}

	public void OnItemAdded(ItemStack itemStack)
	{
		ItemAdded?.Invoke(this, itemStack);
	}

	public void OnItemRemoved(ItemStack itemStack)
	{
		ItemRemoved?.Invoke(this, itemStack);
	}
}
