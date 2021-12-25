using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeftEmptyFullHandler : AbstractHandler
{
	public LeftEmptyFullHandler(NewMouseCursor mouse, NewCraftingModel model) : base(mouse, model) { }

	public override object Handle(int slot)
	{
		if (slot != OUTPUT_SLOT && _mouse.IsEmpty && !_model.IsEmpty(slot))
		{
			ItemStack item = _model.RemoveFromSlot(slot);
			_mouse.AddToSlot(item);
			return null;
		}

		return base.Handle(slot);
	}
}
