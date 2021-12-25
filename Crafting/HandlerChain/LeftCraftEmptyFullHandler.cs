using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCraftEmptyFullHandler : AbstractHandler
{
	public LeftCraftEmptyFullHandler(NewMouseCursor mouse, NewCraftingModel model) : base(mouse, model) { }

	public override object Handle(int slot)
	{
		if (_mouse.IsEmpty && slot == OUTPUT_SLOT)
		{
			if (_model.Output != null)
			{
				_mouse.AddToSlot(_model.Craft());
			}
			return null;
		}

		return base.Handle(slot);
	}
}
