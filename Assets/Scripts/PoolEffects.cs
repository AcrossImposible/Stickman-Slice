using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEffects : MonoBehaviour {

	[SerializeField] GameObject effect;
	[SerializeField] [Range(3,10)] int amountPool = 5;
	List<Effect> pool = new List<Effect>();
	public static Dictionary<string, PoolEffects> effectsList = new Dictionary<string, PoolEffects> ();

	// Use this for initialization
	void Start () {
		for (int i = 0; i < amountPool; i++) {
			Effect e = new Effect ( Instantiate (effect, transform), this);
			pool.Add (e);
		}
		effectsList.Add (effect.name, this);

		//--------------------------------------------------
		Transform trm = Pizdos (effect).transform;
		Transform brooo = Pizdos1 (effect).transform;
	}

	public void Play(Vector3 pos){
		foreach (Effect e in pool) {
			if (!e.use) {
				e.Play (pos);
				break;
			}
		}
	}

	void OnLevelWasLoaded(int level){
		effectsList.Clear ();
	}

	public void RunCorout(IEnumerator reset){
		StartCoroutine (reset);

	}

	public class Effect {
		GameObject effect;
		public bool use;
		Animation animation;
		static PoolEffects poolEffects;

		public Effect ( GameObject effect, PoolEffects poolEffects ){
			this.effect = effect;
			use = false;
			Effect.poolEffects = poolEffects;
			if(effect.GetComponent<Animation>()){
				animation = effect.GetComponent<Animation>();
			}
		}


		public void Play(Vector3 pos) {
			animation.Play ();
			effect.transform.position = pos;
			use = true;
			poolEffects.RunCorout (Reset());
		}

		IEnumerator Reset(){
			yield return new WaitForSeconds (0.8f);
			effect.transform.position = Vector3.zero;
			use = false;
		}


	}

	//TESTO 
	public static T Pizdos<T> (T original) where T : Object {
		return original;
	}

	public static T Pizdos1<T> (T ororo) {
		return ororo;
	}
}
