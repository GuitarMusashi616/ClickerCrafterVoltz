using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour, ISwappable
{
	public InventorySlot floatingSlot;

	public void SetItem(ItemStack itemStack)
	{
		floatingSlot.SetItem(itemStack);
	}

	public ItemStack GetItem()
	{
		return floatingSlot.GetItem();
	}

	public void ClearItem()
	{
		floatingSlot.ClearItem();
	}

	public bool IsEmpty
	{
		get => floatingSlot.IsEmpty;
	}

	public void SetFloatingSlotToCursor()
	{
		var rt = (RectTransform)floatingSlot.transform;
		var vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y + (rt.rect.height / 2));
		floatingSlot.transform.position = vec;
	}


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!floatingSlot.IsEmpty) {
			SetFloatingSlotToCursor();
		}
    }


}
