using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISlotHandler
{
	void SlotClicked(ISlot slot, PointerEventData eventData);
}
