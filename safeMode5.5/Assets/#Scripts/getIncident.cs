using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class getIncident : MonoBehaviour {

	public string url;
	string data;
	public string[] splitString;
	public int num;
	public GameObject map;


	public Text nome;
	public Text estado;
	public Text gravidade;
	public Text distancia;

	public Incident incident;

	public void Go (int num, GameObject map) {
		this.map = map;
		this.num = num;
		url = "https://raw.githubusercontent.com/sebastiao3d/SafeMode/master/%23" + num;
		StartCoroutine(GetNews());
	}

	IEnumerator GetNews() {
		WWW www = new WWW (url);
		while (!www.isDone && www.error == null)
		{
			yield return null;
		}
		data = www.text;

		splitString = data.Split ('\n');

		incident = new Incident (splitString [1], splitString [3], splitString [5], splitString [7]);

		nome.text = incident.tipo;
		estado.text = incident.estado;
		gravidade.text = incident.gravidade;
		int distance = int.Parse (incident.distancia);

		if (distance < 1000) {
			distancia.text = distance + " m";
		} else {
			distancia.text = (distance/1000) + " km";
		}

		string gravidade_str = incident.gravidade;

		switch (gravidade_str) {
			case "Alta":
			map.GetComponent<GoogleMap> ().addElementAlta (float.Parse (splitString [9]), float.Parse (splitString [11]));	
			break;

			case "Média":
			map.GetComponent<GoogleMap> ().addElementMedia (float.Parse (splitString [9]), float.Parse (splitString [11]));
			break;

			case "Baixa":
			map.GetComponent<GoogleMap> ().addElementBaixa (float.Parse (splitString [9]), float.Parse (splitString [11]));
			break;

			case "Nula":
			map.GetComponent<GoogleMap> ().addElementNulo (float.Parse (splitString [9]), float.Parse (splitString [11]));
			break;

			default:
			map.GetComponent<GoogleMap> ().addElementAlta (float.Parse (splitString [9]), float.Parse (splitString [11]));
			break;
		}
	}
}
