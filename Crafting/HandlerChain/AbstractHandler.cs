using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractHandler: IHandler
{
	IHandler _nextHandler;
	protected static int OUTPUT_SLOT = 9;
	protected NewMouseCursor _mouse;
	protected NewCraftingModel _model;

	public AbstractHandler(NewMouseCursor mouse, NewCraftingModel model)
	{
		_mouse = mouse;
		_model = model;
	}

	public virtual object Handle(int slot)
	{
		if (this._nextHandler != null)
		{
			return _nextHandler.Handle(slot);
		}
		else
		{
			return null;
		}
	}

	public IHandler SetNext(IHandler handler)
	{
		_nextHandler = handler;
		return handler;
	}
}
