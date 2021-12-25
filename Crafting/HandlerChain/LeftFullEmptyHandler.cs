using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFullEmptyHandler : AbstractHandler
{
	public LeftFullEmptyHandler(NewMouseCursor mouse, NewCraftingModel model) : base(mouse, model) { }

	public override object Handle(int slot)
	{
		if (slot != OUTPUT_SLOT && !_mouse.IsEmpty && _model.IsEmpty(slot))
		{
			_model.AddToSlot(_mouse.RemoveFromSlot(), slot);
			return null;
		}

		return base.Handle(slot);
	}
}

