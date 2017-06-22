using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum States
{
	Cell, Sheets, Lock, Mirror, CellWithMirror, SheetsWithMirror, LockWithMirror,
	Corridor,
	StairsToCourtyard, StairsToCourtyardWithHairClip, StairsToCourtyardWithClosetUnlocked, Courtyard, Floor, CorridorWithHairclip, CorridorWithClosetUnlocked, CorridorWithUniform, ClosetDoor, InCloset,
	Freedom
}

[Serializable]
public class Transition
{
	public KeyCode key;
	public States state;

#if UNITY_EDITOR
	public string ArrayElementName { get { return UnityEditor.ObjectNames.NicifyVariableName(key.ToString()) + " => " + UnityEditor.ObjectNames.NicifyVariableName(state.ToString()); } }
#endif
}

public class TextController : MonoBehaviour
{

	public Text textElement;
	public List<State> listOfStates;
	public States initialState;

	private Dictionary<States, State> states;
	private States currentState;

	void Start()
	{
		states = new Dictionary<States, State>();
		foreach (var state in listOfStates)
		{
			states.Add(state.state, state);
		}

		currentState = initialState;
	}

	void Update()
	{
		var newState = states[currentState].ExecuteState(textElement);
		if (newState != null)
		{
			currentState = newState.Value;
		}
	}
}
