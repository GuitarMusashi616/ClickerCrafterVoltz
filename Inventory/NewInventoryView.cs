using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class NewInventoryView : MonoBehaviour, ISlotHandler
{
	public event EventHandler<Tuple<ISlot, PointerEventData>> SlotClicked;
	ISlot[] _slots;

	void Start()
	{
		_slots = GetComponentsInChildren<ISlot>();
		SetupSlots();
	}

	void SetupSlots()
	{
		foreach (var slot in _slots)
		{
			slot.SetHandler(this);
		}
	}

	void ISlotHandler.SlotClicked(ISlot slot, PointerEventData eventData)
	{
		SlotClicked?.Invoke(this, Tuple.Create<ISlot, PointerEventData>(slot, eventData));
	}

	public void UpdateUI(NewInventoryModel model)
	{
		Assert.IsTrue(_slots.Length >= model.Count);
		int i = 0;
		foreach (ItemStack itemStack in model)
		{
			_slots[i].SetSlot(itemStack.Item.icon, itemStack.Count);
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
}
