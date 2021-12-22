﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class Slot : MonoBehaviour, IPointerClickHandler, ISlot
{
	public ItemStack _itemStack;
	public List<Item> _items;

	public Image _image;
	public Text _text;
	public ISlotHandler _slotHandler;
	public bool IsEmpty { get; private set; } = true;
	public int SlotNum { get; set; }
	

	public void SetSlot(Sprite sprite, int count)
	{
		SetSprite(sprite);
		SetCount(count);
		IsEmpty = false;
	}

	public void ClearSlot()
	{
		ClearSprite();
		ClearCount();
		IsEmpty = true;
	}

	public void SetHandler(ISlotHandler slotHandler)
	{
		_slotHandler = slotHandler;
	}

	void Start()
	{
		Assert.IsNotNull(_image, $"must set image for {this}");
		Assert.IsNotNull(_text, $"must set textbox for {this}");
	}

	void SetSprite(Sprite sprite)
	{
		_image.sprite = sprite;
		_image.color = Color.white;
	}

	void ClearSprite()
	{
		_image.color = Color.clear;
	}

	void SetCount(int count)
	{
		_text.text = count.ToString();
	}

	void ClearCount()
	{
		_text.text = string.Empty;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		_slotHandler.SlotClicked(this, eventData);
	}
}
