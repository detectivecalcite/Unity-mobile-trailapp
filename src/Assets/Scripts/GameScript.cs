using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour
{
	private Dictionary<int, GameObject> trails = new Dictionary<int, GameObject>();

	void Update()
	{
		// look for all fingers
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);

			// tap: quick touch & release
			if (touch.phase == TouchPhase.Ended && touch.tapCount == 1)
			{
				// touch are screen locations; convert to world
				Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);

				// effect for feedback
				SpecialEffectsScript.MakeExplosion((position));
			}

			// drag
			if (touch.phase == TouchPhase.Began)
			{
				// store this new value
				if (trails.ContainsKey(i) == false)
				{
					Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
					position.z = 0; // make sure trail is visible

					GameObject trail = SpecialEffectsScript.MakeTrail(position);

					if (trail != null)
					{
						Debug.Log(trail);
						trails.Add(i, trail);
					}
				}
			}

			else if (touch.phase == TouchPhase.Moved)
			{
				// move the trail
				if (trails.ContainsKey(i) == false)
				{
					GameObject trail = trails[i];

					Camera.main.ScreenToWorldPoint(touch.position);
					Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
					position.z = 0; // make sure trail is visible
					
					trail.transform.position = position;
				}
			}

			else if (touch.phase == TouchPhase.Ended)
			{
				// clear known trails
				if (trails.ContainsKey(i))
				{
					GameObject trail = trails[i];

					// let the trail fade out
					Destroy (trail, trail.GetComponent<TrailRenderer>().time);
					trails.Remove (i);
				}
			}
		}
	}
}