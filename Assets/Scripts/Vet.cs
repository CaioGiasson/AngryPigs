using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vet {
	public static float Distancia2D ( Vector2 a, Vector2 b ) {
		return Mathf.Sqrt ( ( a.x - b.x ) * ( a.x - b.x ) + ( a.y - b.y ) * ( a.y - b.y ) );
	}

	public static float Distancia ( Vector2 a, Vector2 b ) {
		return Mathf.Sqrt ( ( a.x - b.x ) * ( a.x - b.x ) + ( a.y - b.y ) * ( a.y - b.y ) );
	}
	public static float Distancia ( Vector3 a, Vector3 b ) {
		return Mathf.Sqrt ( ( a.x - b.x ) * ( a.x - b.x ) + ( a.y - b.y ) * ( a.y - b.y ) + ( a.z - b.z ) * ( a.z - b.z ) );
	}

	public static Vector2 Soma ( Vector2 a, Vector2 b ) {
		return new Vector2 (
			a.x + b.x,
			a.y + b.y );
	}

	public static Vector3 Soma ( Vector3 a, Vector3 b ) {
		return new Vector3 (
			a.x + b.x,
			a.y + b.y,
			a.z + b.z
		);
	}

	public static Vector2 Soma ( Vector2[] vetores ) {
		Vector2 result = new Vector2 ( 0, 0 );     // Vector2 e Vector 3 nao sao anulaveis em c#, entao quando nao houver vetores no array, vai retornar o vetor 0
		foreach ( Vector2 v in vetores ) {
			result.x += v.x;
			result.y += v.y;
		}
		return result;
	}

	public static Vector3 Soma ( Vector3[] vetores ) {
		Vector3 result = new Vector3 ( 0, 0, 0 );     // Vector2 e Vector 3 nao sao anulaveis em c#, entao quando nao houver vetores no array, vai retornar o vetor 0
		foreach ( Vector3 v in vetores ) {
			result.x += v.x;
			result.y += v.y;
			result.z += v.z;
		}
		return result;
	}

	public static Vector2 Subtrai ( Vector2 a, Vector2 b ) {
		Vector2 result = new Vector2 ( );
		result.x = a.x - b.x;
		result.y = a.y - b.y;
		return result;
	}

	public static Vector3 Subtrai ( Vector3 a, Vector3 b ) {
		return new Vector3 (
			a.x - b.x,
			a.y - b.y,
			a.z - b.z
		);
	}

	public static Vector2 MultiplicarEscalar ( Vector2 v, float e ) {
		return new Vector2 (
			v.x * e,
			v.y * e
		);
	}

	public static Vector3 MultiplicarEscalar ( Vector3 v, float e ) {
		return new Vector3 (
			v.x * e,
			v.y * e,
			v.z * e
		);
	}

	public static float Magnitude ( Vector2 v ) {
		return Mathf.Sqrt ( v.x * v.x + v.y * v.y );
	}

	public static float Magnitude ( Vector3 v ) {
		return Mathf.Sqrt ( v.x * v.x + v.y * v.y + v.z * v.z );
	}

	public static Vector2 Normalizar ( Vector2 v ) {
		return new Vector2 (
			v.x / Vet.Magnitude ( v ),
			v.y / Vet.Magnitude ( v )
		);
	}

	public static Vector3 Normalizar ( Vector3 v ) {
		return new Vector3 (
			v.x / Vet.Magnitude ( v ),
			v.y / Vet.Magnitude ( v ),
			v.z / Vet.Magnitude ( v )
		);
	}

}