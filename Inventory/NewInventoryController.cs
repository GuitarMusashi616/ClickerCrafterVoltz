using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewInventoryController : MonoBehaviour
{
	NewInventoryModel _model;
	NewInventoryView _view;
	NewMouseCursor _mouse;

    // Start is called before the first frame update
    void Start()
    {
		_model = GetComponent<NewInventoryModel>();
		_view = GetComponent<NewInventoryView>();
		_mouse = FindObjectOfType<NewMouseCursor>();

		_model.ItemAdded += OnItemAdded;
		_model.ItemRemoved += OnItemRemoved;
		_view.SlotClicked += OnSlotClicked;
	}


	void OnItemAdded(object obj, ItemStack itemStack)
	{
		_view.UpdateUI(_model);
	}

	void OnItemRemoved(object obj, ItemStack itemStack)
	{
		_view.UpdateUI(_model);
	}

	void OnLeftClickEmptyHandEmptySlot(int slot) { }

	void OnRightClickEmptyHandEmptySlot(int slot) { }

	void OnLeftClickEmptyHandFullSlot(int slot)
	{
		_mouse.AddToSlot(_model.Remove(slot));
	}

	void OnRightClickEmptyHandFullSlot(int slot)
	{
		int largerHalf = (int)Math.Ceiling(_model[slot].Count / 2f);
		_mouse.AddToSlot(_model.Remove(slot, largerHalf));
	}

	void OnLeftClickFullHandEmptySlot(int slot)
	{
		_model.Add(_mouse.RemoveFromSlot());
	}

	void OnRightClickFullHandEmptySlot(int slot)
	{
		_model.Add(_mouse.RemoveFromSlot(1));
	}

	void OnLeftClickFullHandFullSlot(int slot)
	{
		if (_mouse.ItemSlot?.Item.name == _model[slot].Item.name)
		{
			_model.Add(_mouse.RemoveFromSlot());
			return;
		}
		var temp1 = _model.Remove(slot);
		var temp2 = _mouse.RemoveFromSlot();
		_model.Add(temp2);
		_mouse.AddToSlot(temp1);
	}

	void OnRightClickFullHandFullSlot(int slot)
	{
		if (_mouse.ItemSlot?.Item.name == _model[slot].Item.name)
		{
			_model.Add(_mouse.RemoveFromSlot(1));
			return;
		}
	}


	void OnSlotClicked(object obj, Tuple<ISlot, PointerEventData> tup)
	{
		int slot = tup.Item1.SlotNum;
		switch (tup.Item2.button)
		{
			case PointerEventData.InputButton.Left:
				if (_mouse.IsEmpty && _model.IsEmpty(slot))
				{
					OnLeftClickEmptyHandEmptySlot(slot);
				}
				else if (_mouse.IsEmpty && !_model.IsEmpty(slot))
				{
					OnLeftClickEmptyHandFullSlot(slot);
				}
				else if (!_mouse.IsEmpty && _model.IsEmpty(slot))
				{
					OnLeftClickFullHandEmptySlot(slot);
				}
				else if (!_mouse.IsEmpty && !_model.IsEmpty(slot))
				{
					OnLeftClickFullHandFullSlot(slot);
				}
				break;

			case PointerEventData.InputButton.Right:
				if (_mouse.IsEmpty && _model.IsEmpty(slot))
				{
					OnRightClickEmptyHandEmptySlot(slot);
				}
				else if (_mouse.IsEmpty && !_model.IsEmpty(slot))
				{
					OnRightClickEmptyHandFullSlot(slot);
				}
				else if (!_mouse.IsEmpty && _model.IsEmpty(slot))
				{
					OnRightClickFullHandEmptySlot(slot);
				}
				else if (!_mouse.IsEmpty && !_model.IsEmpty(slot))
				{
					OnRightClickFullHandFullSlot(slot);
				}
				break;
		}
	}
}
