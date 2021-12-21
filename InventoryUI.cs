using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour, ISubscriber
{
	[SerializeField]
	Inventory inventory;
	[SerializeField]
	InventorySlot[] slots;

	public void UpdateFromPublisher(IPublisher publisher)
	{
		int i = 0;
		foreach (var keyVal in inventory.items)
		{
			slots[i].SetItem(keyVal.Value);
			i += 1;
		}
		while (i < slots.Length)
		{
			slots[i].ClearItem();
			i += 1;
		}
	}

	void Start()
	{
		inventory = GameObject.Find("GameInstance").GetComponent<Inventory>();
		inventory.AddSubscriber(this);
		slots = gameObject.GetComponentsInChildren<InventorySlot>();
	}



}
