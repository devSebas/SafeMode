using UnityEngine;
using System.Collections;

public enum canvasType {login, main, eventos, map, reportar, reportado, estadoPessoal, info};

public class CanvasManager : MonoBehaviour {
	public GameObject main;
	public GameObject eventos;
	public GameObject map;
	public GameObject reportar;
	public GameObject reportado;
	public GameObject estadoPessoal;
	public GameObject login;
	public GameObject info;

	public canvasType startup;

	void Start () {
		change (startup);
	}

	public void change(canvasType cnvs) {
		main.SetActive (false);
		eventos.SetActive (false);
		map.SetActive (false);
		reportar.SetActive (false);
		reportado.SetActive (false);
		estadoPessoal.SetActive (false);
		login.SetActive (false);

		if (cnvs == canvasType.main) {
			SetMain ();
		} else if (cnvs == canvasType.eventos) {
			SetEventos ();
		} else if (cnvs == canvasType.map) {
			SetMap ();
		} else if (cnvs == canvasType.reportar) {
			SetReportar ();
		} else if (cnvs == canvasType.reportado) {
			SetReportado ();
		} else if (cnvs == canvasType.estadoPessoal) {
			SetEstadoPessoal ();
		} else if (cnvs == canvasType.login) {
			SetLogin ();
		} else if (cnvs == canvasType.info) {
			SetInfo ();
		}
	}

	void SetMain() {
		main.SetActive (true);
	}

	void SetEventos() {
		eventos.SetActive (true);
	}

	void SetMap() {
		map.SetActive (true);
	}

	void SetReportar() {
		reportar.SetActive (true);
	}

	void SetReportado() {
		reportado.SetActive (true);
	}

	void SetEstadoPessoal() {
		estadoPessoal.SetActive (true);
	}

	void SetLogin() {
		login.SetActive (true);
	}

	void SetInfo() {
		info.SetActive (true);
	}
}
