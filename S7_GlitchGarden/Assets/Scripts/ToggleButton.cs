using UnityEngine;

public class ToggleButton : MonoBehaviour
{
	public event System.EventHandler OnSelectionChanged;

	private bool isSelected = true;
	private SpriteRenderer sprite;

	void Awake()
	{
		sprite = GetComponentInChildren<SpriteRenderer>();
		IsSelected = false;
	}

	public bool IsSelected
	{
		get { return isSelected; }
		set
		{
			if (isSelected == value)
				return;

			isSelected = value;

			sprite.color = isSelected ? Color.white : Color.black;

			var ev = OnSelectionChanged;
			if (ev != null)
				ev(this, System.EventArgs.Empty);
		}
	}

	private void OnMouseDown()
	{
		IsSelected = !IsSelected;
	}
}
