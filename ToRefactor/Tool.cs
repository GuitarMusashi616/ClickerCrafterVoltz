using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Tool : MonoBehaviour, IPointerDownHandler
{
	public List<Sprite> _sprites;
	public Image _image;
	public List<WeightedItem> _woodTierLoot;
	public List<WeightedItem> _stoneTierLoot;
	public List<WeightedItem> _ironTierLoot;
	public List<WeightedItem> _diamondTierLoot;
	Material _material;
	NewInventoryModel _inventory;

	void Start()
	{
		Assert.IsNotNull(_sprites, $"make sure to set up {this}'s sprites");
		Assert.IsNotNull(_image, $"make sure to link {this}'s image");
		_inventory = FindObjectOfType<NewInventoryModel>();
	}

	public Material Material
	{
		get => _material;

		set
		{
			int index = (int)value - 1;
			if (index == -1)
			{
				gameObject.SetActive(false);
				return;
			}
			gameObject.SetActive(true);
			_image.sprite = _sprites[index];
			_material = value;
		}
	}

	public List<WeightedItem> ActiveLootTable
	{
		get
		{
			switch (Material)
			{
				case Material.Wood:
					return _woodTierLoot;
				case Material.Stone:
					return _stoneTierLoot;
				case Material.Iron:
					return _ironTierLoot;
				case Material.Diamond:
					return _diamondTierLoot;
				default:
					return null;
			}
		}
	}

	public ItemStack GetLoot()
	{
		Assert.AreNotEqual(Material, Material.None, $"GetLoot can only be called by wood tier or higher for {this}");
		int total = ActiveLootTable.Sum(x => x._weight);
		int choice = Random.Range(1, total+1);
		
		foreach (var loot in ActiveLootTable)
		{
			choice -= loot._weight;
			if (choice <= 0)
			{
				return new ItemStack(ItemRegistry.Instance[loot._item], 1);
			}
		}
		throw new ArgumentException("Loot should have been picked");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_inventory.Add(GetLoot());
	}
}
