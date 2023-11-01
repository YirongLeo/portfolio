using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Consum")]

public class ConsumableClass : ItemClass
{
	[Header("Consumable")]

	public float healthAdded;

	public override ItemClass GetItem()
	{
		return this;
	}
	public override ToolClass GetTool()
	{
		return null;
	}
	public override MissClass GetMiss()
	{
		return null;
	}
	public override ConsumableClass GetConsumable()
	{
		return this;
	}
}
