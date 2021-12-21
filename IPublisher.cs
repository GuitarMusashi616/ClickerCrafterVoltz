using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPublisher
{
	void AddSubscriber(ISubscriber subscriber);
	void RemoveSubscriber(ISubscriber subscriber);
	void Notify();
}
