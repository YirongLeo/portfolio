using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryManager : MonoBehaviour
{

	[SerializeField] private GameObject slotHolder;
	[SerializeField] private ItemClass itemToAdd;
	[SerializeField] private ItemClass itmeToRemove;

	[SerializeField] private SlotClass[] startingItems;

	public InputActionProperty rightActivate;
	public XRRayInteractor rightRay;

	private SlotClass[] items;
	private GameObject[] slots;

	private SlotClass movingSlot;
	private ItemClass tempSlot;
	private SlotClass originalSlot;
	private int quantitySlot;
	bool isMovingItem;

	private void Start()
	{

		slots = new GameObject[slotHolder.transform.childCount];
		items = new SlotClass[slots.Length];

		//初始化欄位
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new SlotClass();
		}

		for (int i = 0; i < startingItems.Length; i++)
		{
			items[i] = startingItems[i];
		}

		//設定所有slots
		for (int i = 0; i < slotHolder.transform.childCount; i++)
		{
			slots[i] = slotHolder.transform.GetChild(i).gameObject;
		}

		RefreshUI();

		Add(itemToAdd);
		Remove(itmeToRemove);
	}

	private void Update()
	{

	}

	#region Iventory工具
	//更新背包欄位
	public void RefreshUI()
	{
		for(int i = 0; i < slots.Length; i++)
		{
			try
			{
				slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
				slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
				
				//如果可以堆疊會跑出數字
				if (items[i].GetItem().isStackable)
				{
					slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[i].GetQuantity() + "";
				}
				//不能堆疊則清空數字
				else
				{
					slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
				}
			}
			catch
			{
				slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
				slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
				//將沒物品的欄位清空
				slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
			}
		}
	}

	//新增物品至背包
	public bool Add(ItemClass item)
	{
		//items.Add(item);

		//確認inventory是否包含item
		SlotClass slot = Contains(item);

		if (slot != null && slot.GetItem().isStackable)
		{
			slot.AddQuantity(1);
		}
		else
		{
			for(int i = 0; i < items.Length; i++)
			{
				if(items[i].GetItem()==null)
				{
					items[i].AddItem(item, 1);
					break;
				}
			}
		}

		RefreshUI();
		return true;
	}

	//從背包刪除物品
		public bool Remove(ItemClass item)
		{
			//items.Remove(item);

			SlotClass temp = Contains(item);
			if (temp != null)
			{
				if (temp.GetQuantity() > 1)
				{
					temp.SubQuantity(1);
				}
				else
				{
					int slotToRemoveIndex = 0;

					for (int i = 0; i < items.Length; i++)
					{
						if (items[i].GetItem() == item)
						{
							slotToRemoveIndex = i;
							break;
						}
					}

					items[slotToRemoveIndex].Clear();
				}
			}
			else
			{
				return false;
			}

			RefreshUI();
			return true;
		}

	//背包內容物
	public SlotClass Contains(ItemClass item)
	{

		for(int i = 0; i < items.Length; i++)
		{
			if (items[i].GetItem() == item)
			{
				return items[i];
			}
		}

		return null;
	}
	#endregion

	//private bool BeginItemMove()
	//{
	//	originalSlot = GetClosestSlot();

	//	if (originalSlot == null || originalSlot.GetItem() == null)
	//	{
	//		//沒有item移動的情況
	//		return false;
	//	}

	//	movingSlot = new SlotClass(originalSlot);
	//	originalSlot.Clear();
	//	RefreshUI();
	//	return true;
	//}

	//private SlotClass GetClosestSlot()
	//{

	//	rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);
	//	//rightRay.TryGetCurrent3DRaycastHit(out RaycastHit raycastHit, out int raycastEndpointIndex);

	//	Debug.Log(slots.Length);

	//	for (int i = 0; i < slots.Length; i++)
	//	{

	//		Debug.Log(rightPos);
	//		Debug.Log(slots[i].transform.position);
	//		Debug.Log(Vector3.Distance(slots[i].transform.position, rightPos));

	//		if (Vector3.Distance(slots[i].transform.position, rightPos) <= 0.01)
	//		{
	//			return items[i];
	//		}	
	//	}

	//	return null;
	//}

	#region Inventory Slot欄位個別設定
	public void itemzero()
	{
		int i = 0;
		Debug.Log(items[0].GetItem());

		if (items[0].GetItem() != null)
		{
			items[0].Clear();
		}
		else
		{

			items[0].AddItem(tempSlot, quantitySlot);
		}
		RefreshUI();
	}

	public void itemone()
	{
		Debug.Log(items[1].GetItem());
		tempSlot = items[1].GetItem();
		quantitySlot = items[1].GetQuantity();

		if (items[1].GetItem() != null)
		{
			items[1].Clear();
		}
		else
		{
			items[1].AddItem(tempSlot, quantitySlot);
		}
		RefreshUI();
	}
	public void itemtwo()
	{
		Debug.Log(items[2].GetItem());
	}
	public void itemthree()
	{
		Debug.Log(items[3].GetItem());
		tempSlot = items[3].GetItem();
		quantitySlot = items[3].GetQuantity();

		if (items[3].GetItem() != null)
		{
			items[3].Clear();
		}
		else
		{
			items[3].AddItem(tempSlot, quantitySlot);
		}
		RefreshUI();
	}
	public void itemfour()
	{
		Debug.Log(items[4].GetItem());
	}
	public void itemfive()
	{
		Debug.Log(items[5].GetItem());
	}
	public void itemsix()
	{
		Debug.Log(items[6].GetItem());
	}
	public void itemseven()
	{
		Debug.Log(items[7].GetItem());
	}
	public void itemeight()
	{
		Debug.Log(items[8].GetItem());
	}
	public void itemnine()
	{
		Debug.Log(items[9].GetItem());
	}
	public void itemten()
	{
		Debug.Log(items[10].GetItem());
	}
	public void itemeleven()
	{
		Debug.Log(items[11].GetItem());
	}
	public void itemtwelve()
	{
		Debug.Log(items[12].GetItem());
	}
	public void itemthirdteen()
	{
		Debug.Log(items[13].GetItem());
	}
	public void itemtfourteen()
	{
		Debug.Log(items[14].GetItem());
	}
	public void itemfifteen()
	{
		Debug.Log(items[15].GetItem());
	}
	public void itemsixteen()
	{
		Debug.Log(items[16].GetItem());
	}
	public void itemseventeen()
	{
		Debug.Log(items[17].GetItem());
	}
	public void itemeighteen()
	{
		Debug.Log(items[18].GetItem());
	}
	public void itemnineteen()
	{
		Debug.Log(items[19].GetItem());
	}
	public void itemtwenty()
	{
		Debug.Log(items[20].GetItem());
	}
	public void itemtwentyone()
	{
		Debug.Log(items[21].GetItem());
	}
	public void itemtwentytwo()
	{
		Debug.Log(items[22].GetItem());
	}
	public void itemtwentythree()
	{
		Debug.Log(items[23].GetItem());
	}
	public void itemtwentyfour()
	{
		Debug.Log(items[24].GetItem());
	}
	public void itemtwentyfive()
	{
		Debug.Log(items[25].GetItem());
	}
	public void itemtwentysix()
	{
		Debug.Log(items[26].GetItem());
	}
	public void itemtwentyseven()
	{
		Debug.Log(items[27].GetItem());
	}
	public void itemtwentyeight()
	{
		Debug.Log(items[28].GetItem());
	}
	public void itemtwentynine()
	{
		Debug.Log(items[29].GetItem());
	}
	public void itemthirty()
	{
		Debug.Log(items[30].GetItem());
	}
	public void itemthirtyone()
	{
		Debug.Log(items[31].GetItem());
	}
	public void itemthirtytwo()
	{
		Debug.Log(items[32].GetItem());
	}
	public void itemthirtythree()
	{
		Debug.Log(items[33].GetItem());
	}
	public void itemthirtyfour()
	{
		Debug.Log(items[34].GetItem());
	}

	#endregion

}
