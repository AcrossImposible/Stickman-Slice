using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public static IEnumerator CameraMove(Vector3 targetPos, float frame){
		Vector3 camPos = new Vector3 (0, 1.8f, -10);
		float velocity = 0f;
		float timeSmoth = 1.3f/frame;
		Vector3 velocityV = Vector3.zero;

		for (int i = 0; i < frame; i++) {
			yield return null;

			//Camera.main.orthographicSize = Mathf.SmoothDamp (Camera.main.orthographicSize, 5.8f, ref velocity, timeSmoth);
			//Camera.main.transform.position = Vector3.SmoothDamp (Camera.main.transform.position, targetPos, ref velocityV, timeSmoth);
		}
		yield return new WaitForSeconds (0.7f);
		for (int i = 0; i < (frame/2); i++) {
			yield return null;
			//Camera.main.orthographicSize = Mathf.SmoothDamp (Camera.main.orthographicSize, 8, ref velocity, timeSmoth/2);
			//Camera.main.transform.position = Vector3.SmoothDamp (Camera.main.transform.position, camPos, ref velocityV, timeSmoth/2);
		}
		Camera.main.transform.position = new Vector3 (0, 1.8f, -10);
		Camera.main.orthographicSize = 8;/**/


		/*Vector3 camPos = Camera.main.transform.position;
		float stepX = (camPos.x - targetPos.x) / frame;
		float stepY = (camPos.y - targetPos.y) / frame;
		float stepSize = 4f / frame;
		for (int i = 0; i < frame; i++) {
			yield return null;
			Camera.main.transform.position -= new Vector3 (stepX, stepY, 0);
			Camera.main.orthographicSize -= stepSize;
		}
		yield return new WaitForSeconds (0.7f);
		for (int i = 0; i < (frame/2); i++) {
			yield return null;
			Camera.main.transform.position += new Vector3 (stepX*2, stepY*2, 0);
			Camera.main.orthographicSize += stepSize*2;
		}
		Camera.main.transform.position = camPos;
		Camera.main.orthographicSize = 8;/**/
	}
}
