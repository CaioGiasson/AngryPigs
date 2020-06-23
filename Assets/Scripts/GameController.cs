using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController game;
	private void Awake ( ) { game = this; }

	public GameObject bolha;
	public GameObject mar;

	public int arrasto = 0;
	private string[] tiposArrasto = { "NENHUM", "BOLHA", "SUPERFICIE" };

	public float constanteArrasto;
	public float densidadeAr;               // 1.225	kg / m³
	public float densidadeAgua;             // 997		kg / m³

	public Button btnArrasto;
	public Button btnEscalaTempo;

	private void Start ( ) {
		bolha.SetActive ( false );
		mar.SetActive ( false );
		Time.timeScale = 1.0f;
	}

	public void toggleArrasto ( ) {
		arrasto = arrasto < 2 ? arrasto + 1 : 0;

		string txt = "Tipo de arrasto: \n" + tiposArrasto[arrasto];
		btnArrasto.GetComponentInChildren<Text> ( ).text = txt;

		bolha.SetActive ( arrasto == 1 );
		mar.SetActive ( arrasto == 2 );
	}

	public void toggleEscalaTempo ( ) {
		string txt;
		float t = Time.timeScale;
		if ( t > 0.8f ) {
			Time.timeScale = 0.8f;
			txt = "Escala de tempo:\n80%";
		}
		else if ( t > 0.5f ) {
			Time.timeScale = 0.5f;
			txt = "Escala de tempo:\n50%";
		}
		else {
			Time.timeScale = 1.0f;
			txt = "Escala de tempo:\n100%";
		}

		btnEscalaTempo.GetComponentInChildren<Text> ( ).text = txt;
	}

	public float getArrasto ( Vector3 pos, float area ) {

		float densidadeFluidoAtual = 0;

		if ( arrasto == 2 && pos.y < -3.3f ) {
			bolha.GetComponent<SpriteRenderer> ( ).color = Color.white;
			densidadeFluidoAtual = densidadeAgua;
		}
		else if ( arrasto == 1 && Vet.Distancia2D ( pos, bolha.transform.position ) < 2.2 ) {
			bolha.GetComponent<SpriteRenderer> ( ).color = Color.red;
			densidadeFluidoAtual = densidadeAgua;
		}
		else {
			bolha.GetComponent<SpriteRenderer> ( ).color = Color.white;
			densidadeFluidoAtual = densidadeAr;
		}

		// A FORÇA DE ARRASTO SEMPRE TEM SINAL CONTRÁRIO AO DO MOVIMENTO
		// ISSO É REPRESENTADO PELO SINAL - NO COMEÇO DA EQUAÇÃO
		// ESSE SINAL VAI SER TRATADO LÁ NO MOVIMENTO DO CORPO
		return 0.5f * densidadeFluidoAtual * area * constanteArrasto;
	}

}
