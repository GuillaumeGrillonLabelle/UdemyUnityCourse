using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class State
{
	public States state;
	public string blurb;
	public string commands;
	public List<Transition> transitions;

#if UNITY_EDITOR
	public string ArrayElementName { get { return UnityEditor.ObjectNames.NicifyVariableName(state.ToString()); } }
#endif

	public States? ExecuteState(Text text)
	{
		text.text = blurb + "\n\n" + commands;
		foreach (var tr in transitions)
		{
			if (Input.GetKeyDown(tr.key))
			{
				return tr.state;
			}
		}

		return null;
	}
}
