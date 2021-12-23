using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class Crafting : MonoBehaviour, ISlotHandler
{
	const int OutputSlot = 9; 
	MouseCursor _mouse;
	ItemStack[] _items = new ItemStack[10];
	ISlot[] _slots;
	CraftingRecipes _craftingRecipes;

	void Start()
	{
		_slots = GetComponentsInChildren<ISlot>();
		_mouse = FindObjectOfType<MouseCursor>();
		_craftingRecipes = FindObjectOfType<CraftingRecipes>();
		int i = 0;
		foreach (var slot in _slots)
		{
			slot.SetHandler(this);
			slot.SlotNum = i;
			i += 1;
		}
	}

	public void Add(ItemStack itemStack, int slotNum)
	{
		Assert.AreNotEqual(slotNum, OutputSlot, "Must call SetOutput to set output slot");

		_items[slotNum] = itemStack;

		UpdateOutput();
		UpdateUI();
	}

	public ItemStack Remove(int slotNum)
	{
		// Assert.AreNotEqual(slotNum, OutputSlot, "Must call Craft to remove from output slot");
		if (slotNum == OutputSlot)
		{
			return Craft();
		}

		var temp = _items[slotNum];
		_items[slotNum] = null;
		UpdateOutput();
		UpdateUI();
		return temp;
	}

	public ItemStack Craft()
	{
		var temp = _items[OutputSlot];
		_items[OutputSlot] = null;
		ConsumeIngredients();
		UpdateUI();
		return temp;
	}

	public void SetOutput(ItemStack itemStack)
	{
		_items[OutputSlot] = itemStack;
		UpdateUI();
	}

	public void ConsumeIngredients()
	{
		for (int i = 0; i < 9; i++)
		{
			if (_items[i] != null)
			{
				_items[i].Count -= 1;
				if (_items[i].Count < 1)
				{
					_items[i] = null;
				}
			}
		}
		UpdateOutput();
		UpdateUI();
	}

	public string HashIngredients()
	{
		string hash = "";
		for (int i=0; i<9; i++)
		{
			hash += _items[i]?.Item.name + ";";
		}
		return hash;
	}

	public void UpdateOutput()
	{
		try
		{
			ItemStack itemStack = _craftingRecipes.Craft(HashIngredients());
			SetOutput(itemStack);
		}
		catch (AssertionException)
		{
			SetOutput(null);
			return;
		}

	}

	public void UpdateUI()
	{
		for (int i=0; i<_items.Length; i++)
		{
			var item = _items[i];
			if (item != null)
			{
				_slots[i].SetSlot(item.Item.icon, item.Count);
			}
			else
			{
				_slots[i].ClearSlot();
			}
		}
	}

	public void Test()
	{
		print(HashIngredients());

		var rec = new Recipe("Planks", 4, new string[] { null, null, null, null, "Log", null, null, null, null });
		var rec2 = new Recipe("Stick", 4, new string[] { null, null, null, null, "Planks", null, null, "Planks", null });

		Recipe[] recipes = new Recipe[] { rec, rec2 };
		foreach (var recipe in recipes)
		{
			print(JsonUtility.ToJson(recipe));
		}
		// print(rec.Hash());

		_craftingRecipes.Add(rec);
		_craftingRecipes.Add(rec2);
		print(_craftingRecipes[rec].HashIngredients());
	}


	public void LeftClickWithMouse(ISlot slot)
	{
		if (slot.SlotNum == OutputSlot && _items[slot.SlotNum] != null)
		{
			var temp1 = _mouse.GetItem();
			var temp2 = _items[slot.SlotNum];
			if (!_mouse.IsEmpty && temp1.Item.name == temp2.Item.name)
			{
				// full hand, full slot, same name collect output
				_mouse.SetItem(temp1 + Remove(slot.SlotNum));
			}
			else if (_mouse.IsEmpty)
			{
				// empty hand, full slot, collect
				_mouse.SetItem(Remove(slot.SlotNum));
			}
			return;
		}

		if (!_mouse.IsEmpty && _items[slot.SlotNum] != null)
		{
			// full hand, full slot
			var temp1 = _mouse.GetItem();
			var temp2 = _items[slot.SlotNum];
			if (temp1.Item.name == temp2.Item.name)
			{
				// same name items
				Add(temp1 + temp2, slot.SlotNum);
				_mouse.ClearItem();
			}
			else
			{
				// not same name items
				Add(temp1, slot.SlotNum);
				_mouse.SetItem(temp2);
			}
		}
		else if (!_mouse.IsEmpty && _items[slot.SlotNum] == null)
		{
			// full hand, empty slot
			Add(_mouse.GetItem(), slot.SlotNum);
			_mouse.ClearItem();
		}
		else if (_mouse.IsEmpty && _items[slot.SlotNum] != null)
		{
			// empty hand, full slot
			_mouse.SetItem(Remove(slot.SlotNum));
		}

	}

	public void RightClickWithMouse(ISlot slot)
	{
		if (slot.SlotNum == OutputSlot)
		{
			return;
		}

		if (!_mouse.IsEmpty && _items[slot.SlotNum] != null)
		{
			// full hand, full slot
			var temp1 = _mouse.GetItem();
			var temp2 = _items[slot.SlotNum];
			if (temp1.Item.name == temp2.Item.name)
			{
				Add(_mouse.DropItem() + temp2, slot.SlotNum);
			}
		}
		else if (!_mouse.IsEmpty && _items[slot.SlotNum] == null)
		{
			// full hand, empty slot
			Add(_mouse.DropItem(), slot.SlotNum);
		}
		else if (_mouse.IsEmpty && _items[slot.SlotNum] != null)
		{
			// empty hand, full slot
			var temp = Remove(slot.SlotNum);
			if (temp.Count <= 1)
			{
				_mouse.SetItem(temp);
				return;
			}
			int amount_to_take = (int) Math.Ceiling(temp.Count / 2f);
			temp.Count -= amount_to_take;
			_mouse.SetItem(new ItemStack(temp.Item, amount_to_take));
			Add(temp, slot.SlotNum);
		}
	}


	public void SlotClicked(ISlot slot, PointerEventData eventData)
	{
		switch (eventData.button)
		{
			case PointerEventData.InputButton.Left:
				LeftClickWithMouse(slot);
				break;

			case PointerEventData.InputButton.Right:
				RightClickWithMouse(slot);
				break;
		}
		

		//if (_items[slot.SlotNum] == null)
		//{
			
		//	if (_mouse.IsEmpty)
		//	{
		//		// slot empty and hand empty
		//		return;
		//	}
		//	// slot empty hand full
		//	ItemStack item = _mouse.GetItem();
		//	_mouse.ClearItem();
		//	Add(item, slot.SlotNum);
		//	return;
		//}
		//// slot full hand empty
		//if (_mouse.IsEmpty)
		//{
		//	_mouse.SetItem(Remove(slot.SlotNum));
		//	return;
		//}
		//// slot full hand full
		//SwapWithMouse(slot);
	}
}
