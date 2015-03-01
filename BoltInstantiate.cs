/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Bolt Networking")]
	[Tooltip("Instantiate a Bolt Prefab.")]
	public class BoltInstantiate : FsmStateAction
	{
		[Tooltip("Choose a Prefab from the Project Hierarchy.")]
		public FsmGameObject prefab;

		[Tooltip("Optional Spawn Point.")]
		public FsmGameObject spawnPoint;
		
		[Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
		public FsmVector3 position;
		
		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
		public FsmVector3 rotation;

		[Tooltip("Send an event when finished.")]
		public FsmEvent finishEvent;

		[Tooltip("Send an event when finished.")]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeGameObject;

		public override void Reset()
		{
			prefab = null;
			spawnPoint = null;
			position = new FsmVector3 { UseVariable = true };
			rotation = new FsmVector3 { UseVariable = true };
			finishEvent = null;
			storeGameObject = null;
		}

		public override void OnEnter()
		{
			var go = prefab.Value;

			if (go != null)
			{
				var spawnPosition = Vector3.zero;
				var spawnRotation = Vector3.zero;
				
				if (spawnPoint.Value != null)
				{
					spawnPosition = spawnPoint.Value.transform.position;
					
					if (!position.IsNone)
					{
						spawnPosition += position.Value;
					}

					spawnRotation = !rotation.IsNone ? rotation.Value : spawnPoint.Value.transform.eulerAngles;
				}

				else
				{
					if (!position.IsNone)
					{
						spawnPosition = position.Value;
					}
					
					if (!rotation.IsNone)
					{
						spawnRotation = rotation.Value;
					}
				}

				var entity = BoltNetwork.Instantiate(go, spawnPosition, Quaternion.Euler(spawnRotation));
				storeGameObject = entity.gameObject;
			}

			Fsm.Event(finishEvent);
			Finish ();
		}
	}
}