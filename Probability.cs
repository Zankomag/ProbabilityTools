using System.Collections.Generic;
using System.Linq;

namespace Generic {
	public static class Probability {

		private static readonly FastRandom random = new FastRandom();

		public static void ReinitialiseRandom(int seed) {
			random.Reinitialise(seed);
		}

		public static T GetRandomItem<T>(IEnumerable<T> array) {
			return GetRandomItem(array.ToArray());
		}

		public static T GetRandomItem<T>(T[] array) {
			int index = random.Next(0, array.Length);
			return array[index];
		}

		public static T GetRandomItemByProbability<T>(ProbabilityList<T> probabilityList) {
			return probabilityList.GetRandomItem();
		}
	}

}
