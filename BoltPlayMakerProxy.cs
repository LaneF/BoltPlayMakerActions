﻿using UnityEngine;
using Bolt;
using System.Collections;

/// <summary>
/// Bolt play maker proxy.
/// Each Bolt Entity requires one of these. It will handle OnAttached(), general setup and 
/// fire events or code that need to take place here or is better than being duplicated in actions.
/// </summary>
public class BoltPlayMakerProxy : Bolt.EntityBehaviour<IState> {

	public bool doSetTransforms = true;
	public string transformName;

	/// <summary>
	/// Attached this instance.
	/// Attached() is used for when an Entity is attached by Bolt to the Network.... ie, Start() for network.
	/// </summary>
	public override void Attached() {
		if (doSetTransforms){
			((NetworkTransform)state.GetDynamic(transformName)).SetTransforms(gameObject.transform);
		}
	}
}