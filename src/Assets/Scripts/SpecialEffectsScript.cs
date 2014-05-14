using UnityEngine;

public class SpecialEffectsScript : MonoBehaviour
{
	private static SpecialEffectsScript instance;

	// prefabs
	public ParticleSystem explosionEffect;
	public GameObject trailPrefab;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		if (explosionEffect == null)
			Debug.LogError("Missing explosion effect");
		if (trailPrefab == null)
			Debug.LogError("Missing trail prefab");
	}

	public static ParticleSystem MakeExplosion(Vector3 position)
	{
		if (instance == null)
		{
			Debug.LogError("There is no SpecialEffectsScript in the scene");
			return null;
		}

		ParticleSystem effect = Instantiate(instance.explosionEffect) as ParticleSystem;
		effect.transform.position = position;

		// program destruction at the end of the effect
		Destroy (effect.gameObject, effect.duration);

		return effect;
	}

	public static GameObject MakeTrail(Vector3 position)
	{
		if (instance == null)
		{
			Debug.LogError("There is no SpecialEffectsScript in the scene");
			return null;
		}

		GameObject trail = Instantiate(instance.trailPrefab) as GameObject;
		trail.transform.position = position;

		return trail;
	}
}