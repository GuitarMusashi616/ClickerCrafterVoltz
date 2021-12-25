using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class ItemStack: ICloneable
{
	public ItemStack(Item item, int count)
	{
		Item = item;
		Count = count;
	}

	public Item Item { get;}
	public int Count { get; }

	public object Clone()
	{
		return new ItemStack(this.Item, this.Count);
	}

	public override string ToString()
	{
		return $"{Count}x {Item.name}";
	}

	public static ItemStack operator +(ItemStack lhs, ItemStack rhs)
	{
		Assert.AreEqual(lhs.Item.name, rhs.Item.name, "Can only add ItemStack of same name");
		return new ItemStack(lhs.Item, lhs.Count + rhs.Count);
	}
	
	public static ItemStack operator -(ItemStack lhs, ItemStack rhs)
	{
		Assert.AreEqual(lhs.Item.name, rhs.Item.name, "Can only subtract ItemStack of same name");
		Assert.IsTrue(lhs.Count >= rhs.Count, "lhs - rhs, lhs must be greater than or equal to rhs");
		if (lhs.Count == rhs.Count)
		{
			return null;
		}
		return new ItemStack(lhs.Item, lhs.Count - rhs.Count);
	}

	public static ItemStack operator +(ItemStack lhs, int rhs)
	{
		return new ItemStack(lhs.Item, lhs.Count + rhs);
	}

	public static ItemStack operator -(ItemStack lhs, int rhs)
	{
		Assert.IsTrue(lhs.Count >= rhs, "lhs - rhs, lhs must be greater than or equal to rhs");
		if (lhs.Count == rhs)
		{
			return null;
		}

		return new ItemStack(lhs.Item, lhs.Count - rhs);
	}

	public static ItemStack Create(ItemStack reference, int amount)
	{
		Assert.IsTrue(amount >= 0, $"cannot create ItemStack with {amount} items");
		if (amount == 0)
		{
			return null;
		}
		return new ItemStack(reference.Item, amount);
	}
}
