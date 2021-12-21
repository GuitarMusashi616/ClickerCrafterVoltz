using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleGameObject: MonoBehaviour, IPointerDownHandler
{
	public GameObject menu;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (menu.activeSelf)
		{
			menu.SetActive(false);
		} else
		{
			menu.SetActive(true);
		}
	}
}
