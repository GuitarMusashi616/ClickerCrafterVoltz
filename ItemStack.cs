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
	public int Count { get; set; }

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
		Assert.AreEqual(lhs.Item.name, rhs.Item.name, "Can only add ItemStack of same name");
		// Assert.AreEqual(lhs.Count > rhs.Count, "lhs - rhs, lhs must be greater than or equal to rhs");
		return new ItemStack(lhs.Item, lhs.Count - rhs.Count);
	}
}
