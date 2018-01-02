using UnityEngine;
using UnityEngine.UI;

public class DefenderToggleButton : ToggleButton
{
	public GameObject defender;
    
    private void Start()
    {
        var costText = GetComponentInChildren<Text>();
        var defenderScript = defender.GetComponent<Defender>();
        costText.text = defenderScript.defenderCost.ToString();
    }
}
