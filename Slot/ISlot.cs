using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot
{
	void SetSlot(Sprite sprite, int count);
	void ClearSlot();
	void SetHandler(ISlotHandler slotHandler);
	bool IsEmpty { get;}
	int SlotNum { get;}
	void SetActive(bool active);
}
