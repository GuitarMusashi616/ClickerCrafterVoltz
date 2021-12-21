using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwappable
{
	ItemStack GetItem();
	void SetItem(ItemStack itemStack);
	void ClearItem();
}
