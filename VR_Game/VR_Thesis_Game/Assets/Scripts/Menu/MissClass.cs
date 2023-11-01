using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Misc")]

public class MissClass : ItemClass
{
	

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
		return this;
	}
	public override ConsumableClass GetConsumable()
	{
		return null;
	}
}
