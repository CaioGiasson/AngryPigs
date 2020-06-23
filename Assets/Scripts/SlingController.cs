using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlingController : MonoBehaviour {

	public static SlingController sling;
	private void Awake ( ) { sling = this; }

	public Pig porcoPuxando, porcoLancando;
	public GameObject pontoLancamento;
	public float distensaoMaxima, constanteMola;

	public float maxDistancia;
	public Text maxDistanciaTXT;
	public Text anguloDisparo;

	private void FixedUpdate ( ) {

		if ( porcoPuxando != null && Input.GetMouseButtonUp ( 0 ) ) {
			Estilingar ( );
			porcoPuxando = null;
		}

		if ( porcoPuxando != null ) AtualizarPosicaoPorco ( );

		if ( porcoLancando != null ) AplicarForcaPorco ( );

	}

	public void AtualizarDistancia ( Vector2 pos, string dados ) {
		float d = Vet.Distancia2D ( pos, transform.position );
		if ( d <= maxDistancia ) return;

		maxDistancia = d;
		maxDistanciaTXT.text = "Max. dist.: " + d + "m\n" + dados;
	}

	void AtualizarPosicaoPorco ( ) {
		// OBTENDO A POSIÇÃO DO MOUSE
		Vector3 mouseTemp = Input.mousePosition;
		mouseTemp.z = 10.0f;
		Vector3 mouse = Camera.main.ScreenToWorldPoint ( mouseTemp );

		float distancia = Vet.Distancia ( mouse, pontoLancamento.transform.position );
		float angulo = Mathf.Atan2 (
			mouse.y - pontoLancamento.transform.position.y,
			mouse.x - pontoLancamento.transform.position.x
		);
		float distensao = Vet.Distancia2D ( porcoPuxando.transform.position, pontoLancamento.transform.position );
		float moduloForca = distensao * constanteMola;
		anguloDisparo.text = "Angulo: " + ( 180 + angulo * Mathf.Rad2Deg ) + "\nForça: " + moduloForca;

		// LIMITANDO A POSIÇÃO ATÉ A DISTENSÃO MÁXIMA
		if ( distancia > distensaoMaxima ) {
			Vector3 novaPos = new Vector2 (
				distensaoMaxima * Mathf.Cos ( angulo ),
				distensaoMaxima * Mathf.Sin ( angulo )
			);
			porcoPuxando.transform.position = Vet.Soma ( pontoLancamento.transform.position, novaPos );
		}
		else porcoPuxando.transform.position = mouse;
	}

	void AplicarForcaPorco ( ) {
		anguloDisparo.text = "Angulo: -\nForça: -";

		float distensao = Vet.Distancia2D ( porcoLancando.transform.position, pontoLancamento.transform.position );

		if ( distensao < 0.4f ) {
			porcoLancando.queda = true;
			porcoLancando = null;
			return;
		}

		float moduloForca = distensao * constanteMola;
		float aceleracao = moduloForca / porcoLancando.massa;
		float angulo = Mathf.Atan2 (
			porcoLancando.transform.position.y - pontoLancamento.transform.position.y,
			porcoLancando.transform.position.x - pontoLancamento.transform.position.x
		);

		// APLICANDO A ACELERAÇÃO CAUSADA PELA FORÇA ELÁSTICA DO ESTILINGUE
		porcoLancando.Acelerar ( aceleracao, angulo );
	}

	void Estilingar ( ) {
		porcoLancando = porcoPuxando;
		porcoLancando.livre = true;
		porcoPuxando = null;

		float angulo = Mathf.Atan2 (
			porcoLancando.transform.position.y - pontoLancamento.transform.position.y,
			porcoLancando.transform.position.x - pontoLancamento.transform.position.x
		);
		float distensao = Vet.Distancia2D ( porcoLancando.transform.position, pontoLancamento.transform.position );
		float moduloForca = distensao * constanteMola;
		porcoLancando.disparoAngulo = 180.0f + angulo * Mathf.Rad2Deg;
		porcoLancando.disparoForca = moduloForca;
	}

	public void ZerarDados ( ) {
		maxDistancia = 0;
		maxDistanciaTXT.text = "Max. dist.: 0m";
	}

}
