using UnityEngine;
using System.Collections;

public class Incident {

	public string tipo;
	public string estado;
	public string gravidade;
	public string distancia;

	public Incident(string tipo, string state, string gravidade, string distancia) {
		this.tipo = tipo;
		this.estado = state;
		this.gravidade = gravidade;
		this.distancia = distancia;
	}
}
