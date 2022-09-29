using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace AppLayer.Tests.Data
{
    public static class TestDataGenerator
    {
        public static IEnumerable<object[]> GetFeatures()
        {
            yield return new object[]
            {
                new List<Feature>
                {
                    new Feature
                    {
                        Name = Guid.NewGuid().ToString(),
                        Properties = new Dictionary<string, int>
                        {
                            { "A", 3 },
                        }
                    },
                    new Feature
                    {
                        Name = Guid.NewGuid().ToString(),
                        Properties = new Dictionary<string, int>
                        {
                            { "A", 3 },
                        }
                    },
                    new Feature
                    {
                        Name = Guid.NewGuid().ToString(),
                        Properties = new Dictionary<string, int>
                        {
                            { "B", 3 },
                        }
                    }
                },
                6
            };

            yield return new object[]
            {
                new List<Feature>
                {
                    new Feature
                    {
                        Name = Guid.NewGuid().ToString(),
                        Properties = new Dictionary<string, int>
                        {
                            { "B", 3 },
                        }
                    },
                    new Feature
                    {
                        Name = Guid.NewGuid().ToString(),
                        Properties = new Dictionary<string, int>
                        {
                            { "B", 3 },
                        }
                    },
                    new Feature
                    {
                        Name = Guid.NewGuid().ToString(),
                        Properties = new Dictionary<string, int>
                        {
                            { "B", 3 },
                        }
                    }
                },
                0
            };

            yield return new object[]
            {
                new List<Feature>
                {
                    new Feature
                    {
                        Name = Guid.NewGuid().ToString(),
                        Properties = new Dictionary<string, int>
                        {
                            { "A", 3 },
                        }
                    },
                    new Feature
                    {
                        Name = Guid.NewGuid().ToString(),
                        Properties = new Dictionary<string, int>
                        {
                            { "A", -2 },
                        }
                    },
                    new Feature
                    {
                        Name = Guid.NewGuid().ToString(),
                        Properties = new Dictionary<string, int>
                        {
                            { "B", 3 },
                        }
                    }
                },
                1
            };
        }
    }
}