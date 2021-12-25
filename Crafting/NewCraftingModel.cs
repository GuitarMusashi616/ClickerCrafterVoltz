using System;
using UnityEngine;
using UnityEngine.Assertions;

public class NewCraftingModel : MonoBehaviour
{
	CraftingRecipes _craftingRecipes;

	public event EventHandler<int> ItemAdded;
	public event EventHandler<int> ItemRemoved;
	public event EventHandler<int> CraftableItemAdded;
	public event EventHandler<int> CraftableItemRemoved;


	// model always has 9 slots while view may have 4
	ItemStack[] _itemSlots = new ItemStack[9];
	ItemStack _outputSlot;

	public void AddToSlot(ItemStack itemStack, int slot)
	{
		Assert.IsNotNull(itemStack, $"cannot add null itemStack to {this}");


		if (_itemSlots[slot]?.Item.name == itemStack?.Item.name)
		{
			// adding to slot with same name item
			_itemSlots[slot] += itemStack;
			OnItemAdded(slot);
		}
		else if (IsEmpty(slot))
		{
			// slot must be empty
			_itemSlots[slot] = itemStack;
			OnItemAdded(slot);
		}
	}

	public ItemStack RemoveFromSlot(int slot, int maxAmount = int.MaxValue)
	{
		ItemStack itemStack = _itemSlots[slot];
		Assert.IsNotNull(itemStack, $"Cannot remove from empty slot ({slot})");
		if (_itemSlots[slot].Count > maxAmount)
		{
			_itemSlots[slot] = itemStack - maxAmount;
			OnItemRemoved(slot);
			return itemStack - _itemSlots[slot].Count;
		}


		_itemSlots[slot] = null;
		OnItemRemoved(slot);
		return itemStack;
	}

	public ItemStack Craft()
	{
		Assert.IsNotNull(_outputSlot, "Can not call craft if output slot is null");
		var temp = _outputSlot;
		_outputSlot = null;
		ConsumeIngredients();
		return temp;
	}

	public string HashIngredients()
	{
		string hash = "";
		for (int i=0; i<_itemSlots.Length; i++)
		{
			hash += _itemSlots[i]?.Item.name + ';';
		}
		return hash;
	}

	void ConsumeIngredients()
	{
		for (int i = 0; i < _itemSlots.Length; i++)
		{
			if (!IsEmpty(i))
			{
				RemoveFromSlot(i,1);
			}
		}
	}

	public bool CanCraft(out ItemStack product)
	{
		try
		{
			product = _craftingRecipes.Craft(HashIngredients());
			return true;
		}
		catch (AssertionException)
		{
			product = null;
			return false;
		}
	}

	public void UpdateOutput(object obj, int slot)
	{
		ItemStack product;
		if (CanCraft(out product))
		{
			_outputSlot = product;
			OnCraftableItemAdded(slot);
		}
		else
		{
			_outputSlot = null;
			OnCraftableItemRemoved(slot);
		}
	}

	void Start()
	{
		_craftingRecipes = FindObjectOfType<CraftingRecipes>();
		ItemAdded += UpdateOutput;
		ItemRemoved += UpdateOutput;
	}

	public void OnItemAdded(int slot)
	{
		ItemAdded?.Invoke(this, slot);
	}

	public void OnItemRemoved(int slot)
	{
		ItemRemoved?.Invoke(this, slot);
	}

	public void OnCraftableItemAdded(int slot)
	{
		CraftableItemAdded?.Invoke(this, slot);
	}

	public void OnCraftableItemRemoved(int slot)
	{
		CraftableItemRemoved?.Invoke(this, slot);
	}

	public bool IsEmpty(int slot)
	{
		return _itemSlots[slot] == null;
	}

	public ItemStack this[int slot]
	{
		get => _itemSlots[slot];
	}

	public ItemStack Output
	{
		get => _outputSlot;
	}
}
