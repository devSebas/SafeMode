using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoogleMap : MonoBehaviour
{
	public enum MapType
	{
		RoadMap,
		Satellite,
		Terrain,
		Hybrid
	}
	public bool loadOnStart = true;
	public bool autoLocateCenter = true;
	public GoogleMapLocation centerLocation;
	public int zoom = 13;
	public MapType mapType;
	public int size = 512;
	public bool doubleResolution = false;
	public GoogleMapMarker[] markers;
	public GoogleMapPath[] paths;
	private Shader shader;

	int altaLenght = 0;
	int mediaLenght = 0;
	int baixaLenght = 0;
	int nulaLenght = 0;

	void Start() {
		shader = Shader.Find("Unlit/Texture");
		InvokeRepeating("Refresh", 0f, 5f);
		//if(loadOnStart) Refresh();	
	}
	
	public void Refresh() {
		if(autoLocateCenter && (markers.Length == 0 && paths.Length == 0)) {
			Debug.LogError("Auto Center will only work if paths or markers are used.");	
		}
		StartCoroutine(_Refresh());
	}
	
	IEnumerator _Refresh ()
	{
		Debug.Log ("mapRefresh");
		var url = "http://maps.googleapis.com/maps/api/staticmap";
		var qs = "";
		if (!autoLocateCenter) {
			if (centerLocation.address != "")
				qs += "center=" + WWW.UnEscapeURL (centerLocation.address);
			else {
				qs += "center=" + WWW.UnEscapeURL (string.Format ("{0},{1}", centerLocation.latitude, centerLocation.longitude));
			}
		
			qs += "&zoom=" + zoom.ToString ();
		}
		qs += "&size=" + WWW.UnEscapeURL (string.Format ("{0}x{0}", size));
		qs += "&scale=" + (doubleResolution ? "2" : "1");
		qs += "&maptype=" + mapType.ToString ().ToLower ();
		var usingSensor = false;
#if UNITY_IPHONE
		usingSensor = Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running;
#endif
		qs += "&sensor=" + (usingSensor ? "true" : "false");
		
		foreach (var i in markers) {
			qs += "&markers=" + string.Format ("size:{0}|color:{1}|label:{2}", i.size.ToString ().ToLower (), i.color, i.label);
			foreach (var loc in i.locations) {
				if (loc.address != "")
					qs += "|" + WWW.UnEscapeURL (loc.address);
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
			}
		}
		
		foreach (var i in paths) {
			qs += "&path=" + string.Format ("weight:{0}|color:{1}", i.weight, i.color);
			if(i.fill) qs += "|fillcolor:" + i.fillColor;
			foreach (var loc in i.locations) {
				if (loc.address != "")
					qs += "|" + WWW.UnEscapeURL (loc.address);
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
			}
		}

		//GetComponent<Renderer> ().material.shader = shader;
		var req = new WWW (url + "?" + qs);
		yield return req;
		GetComponent<Image> ().sprite = Sprite.Create (req.texture, new Rect (0, 0, req.texture.width, req.texture.height), new Vector2 (0f,0f));
		//GetComponent<Renderer> ().material.mainTexture = req.texture;
	}

	public void addElementAlta(float latitude, float longitude) {
		altaLenght++;
		for (int i = 0; i < altaLenght; i++) {
			markers [0].locations [i].latitude = latitude;
			markers [0].locations [i].longitude = longitude;
		}
	}

	public void addElementMedia(float latitude, float longitude) {
		mediaLenght++;
		for (int i = 0; i < mediaLenght; i++) {
			markers [1].locations [i].latitude = latitude;
			markers [1].locations [i].longitude = longitude;
		}
	}

	public void addElementBaixa(float latitude, float longitude) {
		baixaLenght++;
		for (int i = 0; i < baixaLenght; i++) {
			markers [2].locations [i].latitude = latitude;
			markers [2].locations [i].longitude = longitude;
		}
	}

	public void addElementNulo(float latitude, float longitude) {
		nulaLenght++;
		for (int i = 0; i < nulaLenght; i++) {
			markers [3].locations [i].latitude = latitude;
			markers [3].locations [i].longitude = longitude;
		}
	}
}

public enum GoogleMapColor
{
	black,
	brown,
	green,
	purple,
	yellow,
	blue,
	gray,
	orange,
	red,
	white
}

[System.Serializable]
public class GoogleMapLocation
{
	public string address;
	public float latitude;
	public float longitude;
}

[System.Serializable]
public class GoogleMapMarker
{
	public enum GoogleMapMarkerSize
	{
		Tiny,
		Small,
		Mid
	}
	public GoogleMapMarkerSize size;
	public GoogleMapColor color;
	public string label;
	public GoogleMapLocation[] locations;
	
}

[System.Serializable]
public class GoogleMapPath
{
	public int weight = 5;
	public GoogleMapColor color;
	public bool fill = false;
	public GoogleMapColor fillColor;
	public GoogleMapLocation[] locations;	
}