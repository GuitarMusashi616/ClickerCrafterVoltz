using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class NewCraftingView : MonoBehaviour, ISlotHandler
{
	public event EventHandler<Tuple<ISlot, PointerEventData>> SlotClicked;
	Dictionary<int, ISlot> _slots = new Dictionary<int, ISlot>();

	void Start()
    {
		SetupSlots();
	}

	void SetupSlots()
	{
		ISlot[] slots = GetComponentsInChildren<ISlot>();
		foreach (var slot in slots)
		{
			slot.SetHandler(this);
			_slots[slot.SlotNum] = slot;
		}
	}

	void ISlotHandler.SlotClicked(ISlot slot, PointerEventData eventData)
	{
		SlotClicked?.Invoke(this, Tuple.Create<ISlot, PointerEventData>(slot, eventData));
	}

	public void UpdateSlot(int slot, ItemStack item) // 9 updates the output slot
	{
		if (item == null)
		{
			_slots[slot].ClearSlot();
			return;
		}
		_slots[slot].SetSlot(item.Item.icon, item.Count);
	}
}
