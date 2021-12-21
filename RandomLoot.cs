using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class RandomLoot : MonoBehaviour, IPointerDownHandler
{
	// holds a dictionary of item and count
	[SerializeField]
	public List<Item> potentialPrizes = new List<Item>();
	public int prizePicks = 1;
	Inventory inventory;

	public List<ItemStack> ChooseLoot()
	{
		Assert.IsTrue(potentialPrizes.Count > 0, "cannot pick random loot from empty list");
		var lootcrate = new List<ItemStack>();
		var itemsChosen = new Dictionary<int, int>();

		for (int i=0; i<prizePicks; i++)
		{
			var itemNum = Random.Range(0, potentialPrizes.Count);
			if (!itemsChosen.ContainsKey(itemNum))
			{
				itemsChosen.Add(itemNum, 0);
			}
			itemsChosen[itemNum] += 1;
		}

		foreach (var keyVal in itemsChosen)
		{
			Item loot = potentialPrizes[keyVal.Key];
			lootcrate.Add(new ItemStack(loot, keyVal.Value));
		}

		return lootcrate;
	}

	void Start()
	{
		inventory = GameObject.Find("GameInstance").GetComponent<Inventory>();
	}


	public void OnPointerDown(PointerEventData eventData)
	{
		// Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
		var lootcrate = ChooseLoot();
		foreach (ItemStack item in lootcrate)
		{
			Debug.Log(item);
			inventory.Add(item);
		}
	}
}
