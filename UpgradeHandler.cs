using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
	Inventory _inventory;
	Crafting _crafting;

	public List<GameObject> _buttons = new List<GameObject>();
	public List<Tool> _toolList = new List<Tool>();

	Dictionary<string, Tool> _tools = new Dictionary<string, Tool>();
	//public List<String> _buttonItemNames = new List<string>();
	Dictionary<string, GameObject> _toggleables = new Dictionary<string, GameObject>();
	//Dictionary<string, Delegate> _upgradeTriggers = new Dictionary<string, Delegate>();

	void ToggleAndRemove(string buttonName, ItemStack item)
	{
		_toggleables[buttonName].SetActive(true);
		_inventory.Remove(item);
	}

	void NewTool(string toolName, Tool.Material material, ItemStack item)
	{
		_inventory.Remove(item);
		_tools[toolName].SetMaterial(material);
	}



	void Start()
	{
		_crafting = FindObjectOfType<Crafting>();

		_inventory = FindObjectOfType<Inventory>();
		_inventory.ItemAdded += NewItem;

		
		foreach (var button in _buttons)
		{
			_toggleables[button.name] = button;
		}

		
		foreach (var tool in _toolList)
		{
			_tools[tool.name] = tool;
			print(tool.name);
		}


	}




	// inventory.subscribe(this);

	// gets update from inventory, if achievement item then consume item and change menu
	void NewItem(object sender, ItemStack itemStack)
	{	
		switch (itemStack.Item.name)
		{
			case "Crafting Table":
				ToggleAndRemove("CraftingButton", itemStack);
				_crafting.BigTable();
				break;

			case "Furnace":
				ToggleAndRemove("FurnaceButton", itemStack);
				break;

			case "Wooden Sword":
				NewTool("Sword", Tool.Material.Wood, itemStack);
				break;

			case "Wooden Pickaxe":
				NewTool("Pickaxe", Tool.Material.Wood, itemStack);
				break;

			case "Wooden Axe":
				NewTool("Axe", Tool.Material.Wood, itemStack);
				break;

			case "Wooden Shovel":
				NewTool("Shovel", Tool.Material.Wood, itemStack);
				break;

			case "Stone Sword":
				NewTool("Sword", Tool.Material.Stone, itemStack);
				break;

			case "Stone Pickaxe":
				NewTool("Pickaxe", Tool.Material.Stone, itemStack);
				break;

			case "Stone Axe":
				NewTool("Axe", Tool.Material.Stone, itemStack);
				break;

			case "Stone Shovel":
				NewTool("Shovel", Tool.Material.Stone, itemStack);
				break;
		}
	}
}
