using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCraftFullFullHandler : AbstractHandler
{
	public LeftCraftFullFullHandler(NewMouseCursor mouse, NewCraftingModel model) : base(mouse, model) { }

	public override object Handle(int slot)
	{
		if (!_mouse.IsEmpty && slot == OUTPUT_SLOT)
		{
			if (_mouse.ItemSlot?.Item.name == _model.Output?.Item.name)
			{
				_mouse.AddToSlot(_model.Craft());
			}
			return null;
		}

		return base.Handle(slot);
	}
}
