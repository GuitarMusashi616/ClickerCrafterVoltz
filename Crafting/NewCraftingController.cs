using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.PointerEventData;

public class NewCraftingController: MonoBehaviour
{
	// Model View Controller for crafting table
	NewCraftingModel _model;
	NewCraftingView  _view;
	NewMouseCursor   _mouse;

	LeftEmptyFullHandler  _lief;
	RightEmptyFullHandler _rief;

	const int OUTPUT_SLOT = 9;

    void Start()
    {
		_model = GetComponent<NewCraftingModel>();
		_view = GetComponent<NewCraftingView>();
		_mouse = FindObjectOfType<NewMouseCursor>();

		_model.ItemAdded += OnItemAdded;
		_model.ItemRemoved += OnItemRemoved;
		_model.CraftableItemAdded += OnCraftableItemAdded;
		_model.CraftableItemRemoved += OnCraftableItemRemoved;

		_view.SlotClicked += OnSlotClicked;

		// chain of command handlers
		_lief = new LeftEmptyFullHandler(_mouse, _model);
		var life = new LeftFullEmptyHandler(_mouse, _model);
		var liff = new LeftFullFullHandler(_mouse, _model);
		var lcef = new LeftCraftEmptyFullHandler(_mouse, _model);
		var lcff = new LeftCraftFullFullHandler(_mouse, _model);

		_rief = new RightEmptyFullHandler(_mouse, _model);
		var rife = new RightFullEmptyHandler(_mouse, _model);
		var riff = new RightFullFullHandler(_mouse, _model);


		_lief.SetNext(life).SetNext(liff).SetNext(lcef).SetNext(lcff);
		_rief.SetNext(rife).SetNext(riff);
    }

	void OnItemAdded(object obj, int slot)
	{
		_view.UpdateSlot(slot, _model[slot]);
	}

	void OnItemRemoved(object obj, int slot)
	{
		_view.UpdateSlot(slot, _model[slot]);
	}

	void OnCraftableItemAdded(object obj, int slot)
	{
		_view.UpdateSlot(OUTPUT_SLOT, _model.Output);
	}

	void OnCraftableItemRemoved(object obj, int slot)
	{
		_view.UpdateSlot(OUTPUT_SLOT, _model.Output);
	}

	void OnSlotClicked(object obj, Tuple<ISlot, PointerEventData> tup)
	{
		int slot = tup.Item1.SlotNum;
		InputButton button = tup.Item2.button;

		switch (button)
		{
			case InputButton.Left:
				_lief.Handle(slot);
				break;

			case InputButton.Right:
				_rief.Handle(slot);
				break;
		}
	}
}
