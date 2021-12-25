using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFullFullHandler : AbstractHandler
{
	public LeftFullFullHandler(NewMouseCursor mouse, NewCraftingModel model) : base(mouse, model) { }

	public override object Handle(int slot)
	{
		if (slot != OUTPUT_SLOT && !_mouse.IsEmpty && !_model.IsEmpty(slot))
		{
			if (_mouse.ItemSlot?.Item.name == _model[slot].Item.name)
			{
				_model.AddToSlot(_mouse.RemoveFromSlot(), slot);
				return null;
			}
			var temp1 = _model.RemoveFromSlot(slot);
			var temp2 = _mouse.RemoveFromSlot();
			_model.AddToSlot(temp2, slot);
			_mouse.AddToSlot(temp1);
			return null;
		}

		return base.Handle(slot);
	}
}

