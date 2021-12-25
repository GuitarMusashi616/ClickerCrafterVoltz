using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRegistry : MonoBehaviour
{
	public List<Item> _allItems = new List<Item>();
	Dictionary<string, Item> _itemByName = new Dictionary<string, Item>();
	static ItemRegistry _instance;

	public static ItemRegistry Instance
	{
		get => _instance;
	}

	void Start()
	{
		_instance = this;
		ItemsIntoDict();
	}

	void ItemsIntoDict()
	{
		foreach (Item item in _allItems)
		{
			_itemByName[item.name] = item;
		}
	}

	public Item this[string name]
	{
		get => _itemByName[name];
	}

	public bool ContainsKey(string product)
	{
		return _itemByName.ContainsKey(product);
	}
}
