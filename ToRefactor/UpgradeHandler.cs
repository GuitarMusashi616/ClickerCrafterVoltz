using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
	NewInventoryModel _inventory;
	TransformCraftingGrid _crafting;

	public List<GameObject> _buttons = new List<GameObject>();
	public List<Tool> _toolList = new List<Tool>();

	Dictionary<string, Tool> _tools = new Dictionary<string, Tool>();
	Dictionary<string, GameObject> _toggleables = new Dictionary<string, GameObject>();

	void ToggleAndRemove(string buttonName, string itemName)
	{
		_toggleables[buttonName].SetActive(true);
		_inventory.Remove(itemName, 1);
	}

	void NewTool(string toolName, Material material, string itemName)
	{
		_inventory.Remove(itemName, 1);
		_tools[toolName].Material = material;
	}

	void Start()
	{
		_crafting = FindObjectOfType<TransformCraftingGrid>();
		_inventory = FindObjectOfType<NewInventoryModel>();
		_inventory.ItemAdded += NewItem;

		SetupButtons();
		SetupTools();
	}

	void SetupButtons()
	{
		foreach(var button in _buttons)
		{
			_toggleables[button.name] = button;
		}
	}

	void SetupTools()
	{
		foreach (var tool in _toolList)
		{
			_tools[tool.name] = tool;
		}
	}




	// inventory.subscribe(this);

	// gets update from inventory, if achievement item then consume item and change menu
	void NewItem(object sender, ItemStack itemStack)
	{
		string itemName = itemStack.Item.name;
		switch (itemName)
		{
			case "Crafting Table":
				ToggleAndRemove("CraftingButton", itemName);
				_crafting.BigTable();
				break;

			case "Furnace":
				ToggleAndRemove("FurnaceButton", itemName);
				break;

			case "Wooden Sword":
				NewTool("Sword", Material.Wood, itemName);
				break;

			case "Wooden Pickaxe":
				NewTool("Pickaxe", Material.Wood, itemName);
				break;

			case "Wooden Axe":
				NewTool("Axe", Material.Wood, itemName);
				break;

			case "Wooden Shovel":
				NewTool("Shovel", Material.Wood, itemName);
				break;

			case "Stone Sword":
				NewTool("Sword", Material.Stone, itemName);
				break;

			case "Stone Pickaxe":
				NewTool("Pickaxe", Material.Stone, itemName);
				break;

			case "Stone Axe":
				NewTool("Axe", Material.Stone, itemName);
				break;

			case "Stone Shovel":
				NewTool("Shovel", Material.Stone, itemName);
				break;
		}
	}
}
