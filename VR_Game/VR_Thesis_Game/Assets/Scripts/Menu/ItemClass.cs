using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
	[Header("Item")]
	public string itemName;
	public Sprite itemIcon;
	//設定背包欄的物體可以堆疊
	public bool isStackable = true;

	public abstract ItemClass GetItem();
	public abstract ToolClass GetTool();
	public abstract MissClass GetMiss();
	public abstract ConsumableClass GetConsumable();

}
