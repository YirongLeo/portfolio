using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SlotTest : MonoBehaviour
{

    public GameObject ItemInSlot;
    public Image slotImage;
    Color originalColor;
    public InputActionProperty rightActivate;

    // Start is called before the first frame update
    void Start()
    {
        slotImage = GetComponentInChildren<Image>();
        originalColor = slotImage.color;
    }

	private void OnTriggerStay(Collider other)
	{
		if (ItemInSlot != null)
		{
            return;
		}

        GameObject obj = other.gameObject;

		if (!IsItem(obj))
		{
            return;
		}

		if (rightActivate.action.WasPerformedThisFrame())
		{
            InsertItem(obj);
		}
	}

    bool IsItem(GameObject obj)
	{
        return obj.GetComponent<Itemtest>();
	}

    void InsertItem(GameObject obj)
	{
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Itemtest>().slotRotation;
        obj.GetComponent<Itemtest>().inSlot = true;
        obj.GetComponent<Itemtest>().currentSlot = this;
        ItemInSlot = obj;
        slotImage.color = Color.gray;

    }
    
    public void ResetColor()
	{
        slotImage.color = originalColor;
	}
    
}
