using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CraftingRecipes : MonoBehaviour
{
	const string Separator = "##";
	// check if its a recipe real fast
	public TextAsset _jsonText;
	public Dictionary<string, Recipe> _recipes = new Dictionary<string, Recipe>();
	public List<Item> _staticItems = new List<Item>();
	Dictionary<string, Item> _itemLookup = new Dictionary<string, Item>();

	void Start()
	{
		Assert.IsNotNull(_jsonText, "Assign Json Folder");
		Assert.IsTrue(_staticItems.Count > 0, "Put Items that can be crafted in static items");
		foreach (var item in _staticItems)
		{
			_itemLookup.Add(item.name, item);
		}
		ReadJson();
	}

	public void Add(Recipe recipe)
	{
		Assert.AreNotEqual(recipe._product, string.Empty, "recipe output must be initialized");
		Assert.AreNotEqual(recipe._output, 0, "recipe output must be initialized");
		Assert.IsTrue(_itemLookup.ContainsKey(recipe._product), $"{recipe._product} not in {_staticItems}");
		_recipes.Add(recipe.HashIngredients(), recipe);
	}

	public bool Contains(string hash)
	{
		return _recipes.ContainsKey(hash);
	}

	public Recipe this[string hash]
	{
		get => _recipes[hash];
	}

	public Recipe this[Recipe recipe]
	{
		get => _recipes[recipe.HashIngredients()];
	}

	public ItemStack Craft(string hash)
	{
		Assert.IsTrue(Contains(hash), $"{hash} not in _recipes");

		string product = _recipes[hash]._product;

		Item item = _itemLookup[product];

		return new ItemStack(item, _recipes[hash]._output);
	}

	void ReadJson()
	{
		string[] result = _jsonText.text.Split(new[] { Separator }, StringSplitOptions.None);
		foreach (var obj in result)
		{
			var recipe = JsonUtility.FromJson<Recipe>(obj);
			Add(recipe);
		}
	}

}
