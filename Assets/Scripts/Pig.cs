using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pig : MonoBehaviour {

	public float massa, gravidade, areaSecaoTransversal;
	public bool livre = false;
	public bool queda = false;

	public float disparoAngulo;
	public float disparoForca;

	private float Vx, Vy;
	private bool arremessado;
	private SpriteRenderer sr;

	private void Start ( ) {
		sr = GetComponent<SpriteRenderer> ( );
	}

	private void OnMouseDown ( ) {
		if ( !arremessado && SlingController.sling.porcoPuxando == null ) {
			SlingController.sling.porcoPuxando = this;
		}
	}

	public void Acelerar ( float aceleracao, float angulo ) {
		// USANDO NEGATIVO POR QUE O ANGULO FORNECIDO É O ANGULO DA DISTENSÃO, E NÃO O DA ACELERAÇÀO
		Vx -= aceleracao * Time.fixedDeltaTime * Mathf.Cos ( angulo );
		Vy -= aceleracao * Time.fixedDeltaTime * Mathf.Sin ( angulo );
	}

	private void FixedUpdate ( ) {

		if ( livre ) {
			// SE ELE NÃO ESTIVER SENDO SEGURADO PELO USUÁRIO PODE SOFRER A ACELERAÇÃO DA GRAVIDADE
			if ( queda )
				Vy += gravidade * Time.fixedDeltaTime;

			// VERIFICANDO SE É PRA USAR OU NÃO A RESISTÊNCIA DOS FLUIDOS
			if ( GameController.game.arrasto > 0 ) {

				float arrasto = GameController.game.getArrasto ( transform.position, areaSecaoTransversal );

				// A FORÇA DE ARRASTO SEMPRE TEM SINAL CONTRÁRIO AO DO MOVIMENTO
				// ISSO É REPRESENTADO PELO SINAL - NO COMEÇO DA EQUAÇÃO
				int Sx = Vx > 0 ? -1 : 1;
				int Sy = Vy > 0 ? -1 : 1;

				// PRIMEIRO CALCULAR A FORÇA DE ARRASTO
				float Fx = Sx * Vx * Vx * arrasto;
				float Fy = Sy * Vy * Vy * arrasto;

				// CALCULANDO AS ACELERAÇÕES RESULTANTES EM CADA COMPONENTE
				float Ax = Fx / massa;
				float Ay = Fy / massa;

				// APLICANDO ESSAS ACELERAÇÕES POR V = V0 + A * t
				// Sx E Sy SÃO OS SINAIS CONTRÁRIOS AO SENTIDO DO MOVIMENTO
				// POR ISSO ELES POSSUEM SINAL ARITMÉTICO CONTRÁRIO AO DA VELOCIDADE
				Vx = Vx + Ax * Time.deltaTime;
				Vy = Vy + Ay * Time.deltaTime;
			}

			transform.position = new Vector3 (
				transform.position.x + Vx * Time.deltaTime,
				transform.position.y + Vy * Time.deltaTime,
				10
			);
		}


		if ( livre && !arremessado && transform.position.y < -6 ) Congelar ( );
	}

	void Congelar ( ) {
		// CRIANDO O PRÓXIMO PORCO PARA SER LANÇADO
		Pig p = Instantiate ( this );
		p.livre = false;
		p.arremessado = false;
		p.queda = false;
		p.transform.position = SlingController.sling.pontoLancamento.transform.position;

		livre = false;
		arremessado = true;
		sr.color = Color.gray;

		string dados = "Angulo: " + disparoAngulo + "\nForça: " + disparoForca;
		SlingController.sling.AtualizarDistancia ( transform.position, dados );
	}
}
