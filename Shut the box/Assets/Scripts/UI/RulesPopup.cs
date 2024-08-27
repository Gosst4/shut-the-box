using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesPopup : MonoBehaviour
{
	public void OnCloseClick()
	{
		Destroy(gameObject);
	}
}
