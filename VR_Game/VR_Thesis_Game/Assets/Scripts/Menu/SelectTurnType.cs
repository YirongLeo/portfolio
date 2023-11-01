using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectTurnType : MonoBehaviour
{

    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;
    public void setTypeFromIndex(int index)
	{
		if (index == 0)
		{
			snapTurn.enabled = false;
			continuousTurn.enabled = true;
		}
		else if (index == 1)
		{
			snapTurn.enabled = true;
			continuousTurn.enabled = false;
		}
	}
}
