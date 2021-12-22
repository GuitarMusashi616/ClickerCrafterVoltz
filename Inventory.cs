using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class Inventory : MonoBehaviour, ISlotHandler
{
	// Dictionary of Inventory Items
	// Uses new UI only slot to display items
	public SortedList<string, ItemStack> _items = new SortedList<string, ItemStack>();
	MouseCursor _mouse;
	ISlot[] _slots;

	void Start()
	{
		_slots = GetComponentsInChildren<ISlot>();
		_mouse = FindObjectOfType<MouseCursor>();
		int i = 0;
		foreach (var slot in _slots)
		{
			slot.SetHandler(this);
			slot.SlotNum = i;
			i += 1;
		}
	}

	public void Add(ItemStack itemStack)
	{
		var name = itemStack.Item.name;
		var count = itemStack.Count;

		if (!_items.ContainsKey(name))
		{
			_items[name] = itemStack;
		}
		else
		{
			_items[name] += itemStack;
		}
		UpdateUI();
	}

	public void Remove(ItemStack itemStack)
	{
		var name = itemStack.Item.name;
		var count = itemStack.Count;

		if (_items.ContainsKey(name))
		{
			if (count >= _items[name].Count)
			{
				_items.Remove(name);
			}
			else
			{
				_items[name] -= itemStack;
			}
		}
		UpdateUI();
	}


	public void UpdateUI()
	{
		int i = 0;
		foreach (var keyVal in _items)
		{
			_slots[i].SetSlot(keyVal.Value.Item.icon, keyVal.Value.Count);
			i += 1;
		}
		while (i < _slots.Length)
		{
			if (_slots[i].IsEmpty)
			{
				break;
			}
			_slots[i].ClearSlot();
			i += 1;
		}
	}

	public void SlotClicked(ISlot slot, PointerEventData eventData)
	{
		// print($"Slot Clicked {slot} {eventData}");

		try
		{
			ItemStack ourItem = _items.ElementAt(slot.SlotNum).Value;
			var item = _mouse.GetItem();
			if (item != null)
			{
				// full hand full slot
				var temp = _mouse.GetItem();
				_mouse.ClearItem();
				Add(temp);
			}
			// empty hand full slot
			_mouse.SetItem(ourItem);
			Remove(ourItem);

		}
		catch (ArgumentOutOfRangeException e)
		{
			// empty slot
			var item = _mouse.GetItem();
			if (item == null)
			{
				return; // empty hand and empty slot just return
			}

			// full hand empty slot
			Add(item);
			_mouse.ClearItem();
			return;
		}
	}
}
