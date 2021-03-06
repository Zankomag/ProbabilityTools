using System.Collections.Generic;
using System.Linq;
using System;

namespace Generic {
	public static class Probability {

		private static readonly Random random = new Random();

		public static void ReinitialiseRandom(int seed) {
			random.Reinitialise(seed);
		}

		public static T GetRandomItem<T>(this IEnumerable<T> array) {
			return GetRandomItem(array.ToArray());
		}

		public static T GetRandomItem<T>(this T[] array) {
			int index = random.Next(0, array.Length);
			return array[index];
		}

		public static T GetRandomItemByProbability<T>(ProbabilityList<T> probabilityList) {
			return probabilityList.GetRandomItem();
		}
		
		/// <summary>
		/// Returns true or false using probability
		/// </summary>
		/// <param name="probability">number from 0 to 1</param>
		public static bool ToBoolByProbability(this double probability) {
			return probability >= random.NextDouble();
		}
	}

}
