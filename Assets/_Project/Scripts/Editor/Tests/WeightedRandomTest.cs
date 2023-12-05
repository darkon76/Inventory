using System.Collections.Generic;
using NUnit.Framework;
using Utils;

namespace Editor.Tests
{
    [TestFixture]
    public class WeightedRandomTest
    {
        private class TestWeight: IWeight
        {
            
            public TestWeight(string name, int weight)
            {
                Name = name;
                Weight = weight;
            }
            public string Name { get; }
            public int Weight { get; }
        }
        
        [TestCase(1,1)]
        [TestCase(1,1,1)]
        [TestCase(1,1,2)]
        [TestCase(1,9)]
        [TestCase(9,1), Repeat(10)]
        [TestCase(1,100)]
        public void TestNumber(params int[] weights)
        {
            var testWeights = new Dictionary<TestWeight, int>();
            var weightedRandom = new WeightedRandom<TestWeight>();
            for (int index = 0; index < weights.Length; index++)
            {
                var weight = weights[index];
                var testWeight = new TestWeight($"Weight {index}", weight);
                testWeights.Add(testWeight, 0);
                weightedRandom.Add(testWeight);
            }

            for (int i = 0; i < 100000; i++)
            {
                var item = weightedRandom.GetItem();
                var value = testWeights[item];
                value++;
                testWeights[item] = value;
            }

            foreach (var pair in testWeights)
            {
                TestContext.WriteLine($"{pair.Key.Name} : {pair.Value.ToString("N0")} ");
            }
        }
    }
}