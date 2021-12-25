using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFullFullHandler : AbstractHandler
{
	public RightFullFullHandler(NewMouseCursor mouse, NewCraftingModel model) : base(mouse, model) { }


	public override object Handle(int slot)
	{
		if (slot != OUTPUT_SLOT && !_mouse.IsEmpty && !_model.IsEmpty(slot))
		{
			if (_mouse.ItemSlot?.Item.name == _model[slot].Item.name)
			{
				_model.AddToSlot(_mouse.RemoveFromSlot(1), slot);
				return null;
			}
			return null;
		}

		return base.Handle(slot);
	}
}

