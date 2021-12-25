using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightEmptyFullHandler : AbstractHandler
{
	public RightEmptyFullHandler(NewMouseCursor mouse, NewCraftingModel model) : base(mouse, model) { }

	public override object Handle(int slot)
	{
		if (slot != OUTPUT_SLOT && _mouse.IsEmpty && !_model.IsEmpty(slot))
		{
			int largerHalf = (int) Math.Ceiling(_model[slot].Count / 2f);
			ItemStack item = _model.RemoveFromSlot(slot, largerHalf);
			_mouse.AddToSlot(item);
			return null;
		}

		return base.Handle(slot);
	}
}
