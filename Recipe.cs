using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Recipe
{
	public string _product;
	public int _output;
	public string[] _ingredients = new string[9];

	public Recipe(string product, int output, string[] ingredients)
	{
		_product = product;
		_output = output;
		_ingredients = ingredients;
	}

	public string HashIngredients()
	{
		string result = "";

		for (int i=0; i<9; i++)
		{
			try
			{
				result += _ingredients[i] + ';';
			}
			catch (IndexOutOfRangeException)
			{
				result += ';';
			}
		}
		return result;
	}

	public override string ToString()
	{
		var str = $"{_output}x {_product} <- ";
		foreach(var ing in _ingredients)
		{
			str += ing + ", ";
		}
		return str;
	}
}