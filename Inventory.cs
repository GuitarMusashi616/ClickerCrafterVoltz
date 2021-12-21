using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assumes item names are unique
public class Inventory : MonoBehaviour, IPublisher
{
	public Dictionary<string, ItemStack> items = new Dictionary<string, ItemStack>();
	public List<ISubscriber> subscribers = new List<ISubscriber>();

	public void Add(ItemStack itemStack)
	{
		var name = itemStack.Item.name;
		var count = itemStack.Count;

		if (!items.ContainsKey(name))
		{
			items[name] = itemStack;
		}
		else
		{
			items[name] += itemStack;
		}
		Notify();
	}

	public void LogContents()
	{
		foreach (var keyVal in items)
		{
			Debug.Log($"{keyVal.Key},\t{keyVal.Value}");
		}
	}

	public void AddSubscriber(ISubscriber subscriber)
	{
		subscribers.Add(subscriber);
	}

	public void Notify()
	{
		foreach(var subscriber in subscribers)
		{
			subscriber.UpdateFromPublisher(this);
		}
	}

	public void RemoveSubscriber(ISubscriber subscriber)
	{
		subscribers.Remove(subscriber);
	}
}
