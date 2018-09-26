using UnityEngine;
using System.Collections;

public class createEvents : MonoBehaviour {

	public string url;
	string data;
	string[] splitString;
	public GameObject incidente;
	public int num;
	public GameObject map;

	void Start () {
		StartCoroutine(GetNumEventos());
	}

	IEnumerator GetNumEventos() {
		WWW www = new WWW (url);
		while (!www.isDone && www.error == null)
		{
			yield return null;
		}
		data = www.text;

		splitString = data.Split ('\n');

		num = int.Parse (splitString [0]);

		RectTransform transformRect = GetComponent<RectTransform> ();
		float minTrans = 0f;

		for (int i = 0; i < num; i++) {
			GameObject a = Instantiate (incidente);
			a.GetComponent<getIncident> ().Go (i+1, map);
			a.transform.SetParent(transform, false);
			a.transform.localPosition = new Vector3 (0f, -60f, 0f);
            a.transform.Translate (new Vector3 (0f, -350f * i, 0f), transform);
			minTrans -= 30f;
		}

		transformRect.offsetMin = new Vector2(transformRect.offsetMin.x, minTrans);
	}
}