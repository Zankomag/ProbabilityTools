using System;
using System.Collections.Generic;
using System.Linq;

namespace Generic {
	public class Probability {

		public static T GetRandomItem<T>(IEnumerable<T> array) {
			return GetRandomItem(array.ToArray());
		}

		public static T GetRandomItem<T>(T[] array) {
			int index = new Random().Next(0, array.Length);
			return array[index];
		}

		public static T GetRandomItemByProbability<T>(ProbabilityList<T> probabilityList) {
			return probabilityList.GetRandomItem();
		}
	}

	public class ProbabilityList<T> {

		private Dictionary<T, double> items;
		public double TotalProbability { get; set; } = 0.0d;


		public ProbabilityList() {
			items = new Dictionary<T, double>();
		}

		/// <param name="items">Item-Chance pair</param>
		public ProbabilityList(Dictionary<T, double> items) {
			this.items = items;
			foreach (var item in items) {
				AddProbability(item.Value);
			}
		}

		public static implicit operator ProbabilityList<T>(Dictionary<T, double> items) => new ProbabilityList<T>(items);

		/// <param name="item">Item which can be picked</param>
		/// <param name="probability">chance of <paramref name="item"/> to be picked. Inclusive from 0.01 to 1.0/></param>
		public void Add(T item, double probability) {
			AddProbability(probability);
			items.Add(item, probability);
		}

		public void Remove(T item) {
			TotalProbability -= items[item];
			items.Remove(item);
		}

		private void AddProbability(double probability) {
			if (probability <= 0.0d)
				throw new ArgumentException("Probability must be greater than 0");
			TotalProbability += probability;
		}

		/// <summary>
		/// A random item will be pickes using its chanse.
		/// </summary>
		public T GetRandomItem() {
			if (TotalProbability == 0.0d)
				throw new ArgumentNullException(typeof(ProbabilityList<T>).ToString(), "There is no item in list");

			T result = default;
			if (TotalProbability < 1.0d) {
				T lastItem = items.Last().Key;
				items[lastItem] += (1.0d - TotalProbability);
				TotalProbability = 1.0d;
			}
			double randomPercent = new Random().NextDouble() + 0.01d; //randomPercent should be >=0.01 and <= 1.0, so 0.01d is added 
			int itemsCount = items.Count;
			double currentItemProbabilityMaxValue = 0.0d;

			foreach (var item in items) {
				currentItemProbabilityMaxValue += item.Value;
				if (currentItemProbabilityMaxValue > 1.0d)
					currentItemProbabilityMaxValue = 1.0d;

				if (randomPercent <= currentItemProbabilityMaxValue) {
					//DEBUG===========================================================
					Console.WriteLine(item.Key + "\t" + randomPercent + "\t" + currentItemProbabilityMaxValue);
					result = item.Key;
					break;
				}
			}

			return result;
		}
	}
}
