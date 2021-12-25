using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TransformCraftingGrid : MonoBehaviour
{
	public List<GameObject> _slots;
	RectTransform _rect;
	RectTransform _rect2;


	public void SmallTable()
	{
		Transform(400, 200, 125, 125);
		foreach (var slot in _slots)
		{
			slot.SetActive(false);
		}
	}

	public void BigTable()
	{
		Transform(450, 250, 185, 185);
		foreach (var slot in _slots)
		{
			slot.SetActive(true);
		}
		
	}

	void Transform(int width, int height, int tableWidth, int tableHeight)
	{
		_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tableWidth);
		_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tableHeight);
		_rect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		_rect2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
	}

	void Start()
    {
		Assert.AreNotEqual(_slots.Count, 0, "Must set slots to transform crafting grid");
		var reactants = GameObject.Find("Reactants");
		_rect = reactants.GetComponent<RectTransform>();
		_rect2 = gameObject.GetComponent<RectTransform>();

		SmallTable();
    }
}
