using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class Tool : MonoBehaviour
{
	public List<Sprite> _sprites;
	public Image _image;

	void Start()
	{
		Assert.IsNotNull(_sprites, $"make sure to set up {this}'s sprites");
		Assert.IsNotNull(_image, $"make sure to link {this}'s image");
	}

	public enum Material
	{
		None,
		Wood,
		Stone,
		Iron,
		Diamond,
	}

	public void SetMaterial (Material material)
	{
		int index = (int)material-1;
		if (index == -1)
		{
			gameObject.SetActive(false);
			return;
		}
		gameObject.SetActive(true);
		_image.sprite = _sprites[index];
	}
}
