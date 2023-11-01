using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
	[Header("Item")]
	public string itemName;
	public Sprite itemIcon;
	//�]�w�I�]�檺����i�H���|
	public bool isStackable = true;

	public abstract ItemClass GetItem();
	public abstract ToolClass GetTool();
	public abstract MissClass GetMiss();
	public abstract ConsumableClass GetConsumable();

}
