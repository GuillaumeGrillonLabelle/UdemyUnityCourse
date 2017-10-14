using UnityEngine;

public class DefendersSelection : MonoBehaviour
{
	private DefenderToggleButton[] defenderButtons;
	private DefenderToggleButton selected;

	void Start()
	{
		defenderButtons = GetComponentsInChildren<DefenderToggleButton>();
		foreach (var button in defenderButtons)
		{
			button.OnSelectionChanged += On_Button_Selection_Changed;
		}
	}

	public DefenderToggleButton Selected { get { return selected; } }

	private void On_Button_Selection_Changed(object sender, System.EventArgs e)
	{
		var newButton = sender as DefenderToggleButton;
		if (newButton.IsSelected)
		{
			foreach (var button in defenderButtons)
			{
				if (button != newButton)
				{
					button.IsSelected = false;
				}
			}
		}
		else
		{
			newButton = null;
		}

		selected = newButton;
	}
}
