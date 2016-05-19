//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

	/// <summary>
	/// Miscellaneous helper stuff for nodes.
	/// </summary>
	public class NodeUtility {

		static Texture2D s_WhiteTexture;

		/// <summary>
		/// Get a white texture.
		/// </summary>
		public static Texture2D whiteTexture {
			get {
				if (s_WhiteTexture == null) {
					s_WhiteTexture = new Texture2D(1,1);
					s_WhiteTexture.SetPixel(0,0,Color.white);
					s_WhiteTexture.Apply();
				}
				return s_WhiteTexture;
			}
		}

		/// <summary>
		/// Returns a random index for an array of weights.
		/// <param name="weights">An array of relative weights for the respective index, the weight value should be greater or equal to zero.</param>
		/// <returns>A random index on the weights array; if the array is empty returns -1.</returns>
		/// </summary>
		public static int GetRandomIndexFor (FloatVar[] weights) {
			// Calculates the sum of weights
			float sumOfWeights = 0f;
			for (int i = 0; i < weights.Length; i++)
				sumOfWeights += weights[i].Value;

			// Get a random number
			float randomNumber = Random.Range(0f, sumOfWeights);

			// Searches for the index
			for (int i = weights.Length - 1; i >= 0; i--) {
				sumOfWeights -= weights[i].Value;
				if (randomNumber >= sumOfWeights)
					return i;
			}

			return -1;
		}

		/// <summary>
		/// Returns a random index for an array of weights.
		/// <param name="weights">An array of relative weights for the respective index, the weight value should be greater or equal to zero.</param>
		/// <returns>A random index on the weights array; if the array is empty returns -1.</returns>
		/// </summary>
		public static int GetRandomIndexFor (float[] weights) {
			// Calculates the sum of weights
			float sumOfWeights = 0f;
			for (int i = 0; i < weights.Length; i++)
				sumOfWeights += weights[i];

			// Get a random number
			float randomNumber = Random.Range(0f, sumOfWeights);

			// Searches for the index
			for (int i = weights.Length - 1; i >= 0; i--) {
				sumOfWeights -= weights[i];
				if (randomNumber >= sumOfWeights)
					return i;
			}

			return -1;
		}
	}
}