using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace Infrastructure.Tests.Data
{
    public class TestDataGenerator
    {
        public static IEnumerable<object[]> GetCurrentValueFromDataGenerator()
        {
            yield return new object[]
            {
                new Character
                {
                    Name = Guid.NewGuid().ToString(),
                    Class = Guid.NewGuid().ToString(),
                    Attributes = new Dictionary<string, AttributeValue>
                    {
                        { "A", new AttributeValue { Value = 3 } },
                    },
                    Features = new List<Feature>
                    {
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "A", 3 },
                            }
                        },
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "A", 3 },
                            }
                        }
                    }
                },
                9
            };

            yield return new object[]
            {
                new Character
                {
                    Name = Guid.NewGuid().ToString(),
                    Class = Guid.NewGuid().ToString(),
                    Attributes = new Dictionary<string, AttributeValue>
                    {
                        { "A", new AttributeValue { Value = 3 } },
                    },
                    Features = new List<Feature>
                    {
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "B", 3 },
                            }
                        },
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "B", 3 },
                            }
                        }
                    }
                },
                3
            };

            yield return new object[]
            {
                new Character
                {
                    Name = Guid.NewGuid().ToString(),
                    Class = Guid.NewGuid().ToString(),
                    Attributes = new Dictionary<string, AttributeValue>
                    {
                        { "A", new AttributeValue { Value = 3 } },
                    },
                    Features = new List<Feature>
                    {
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "B", 3 },
                            }
                        },
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "A", -2 },
                            }
                        }
                    }
                },
                1
            };
        }

        public static IEnumerable<object[]> GetCurrentMaxValueFromDataGenerator()
        {
            yield return new object[]
            {
                new Character
                {
                    Name = Guid.NewGuid().ToString(),
                    Class = Guid.NewGuid().ToString(),
                    Attributes = new Dictionary<string, AttributeValue>
                    {
                        { "A", new AttributeValue { Value = 3, MaxValue = 2 } },
                    },
                    Features = new List<Feature>
                    {
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "A", 3 },
                            }
                        },
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "A", 3 },
                            }
                        }
                    }
                },
                8
            };

            yield return new object[]
            {
                new Character
                {
                    Name = Guid.NewGuid().ToString(),
                    Class = Guid.NewGuid().ToString(),
                    Attributes = new Dictionary<string, AttributeValue>
                    {
                        { "A", new AttributeValue { Value = 3 } },
                    },
                    Features = new List<Feature>
                    {
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "B", 3 },
                            }
                        },
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "B", 3 },
                            }
                        }
                    }
                },
                3
            };

            yield return new object[]
            {
                new Character
                {
                    Name = Guid.NewGuid().ToString(),
                    Class = Guid.NewGuid().ToString(),
                    Attributes = new Dictionary<string, AttributeValue>
                    {
                        { "A", new AttributeValue { Value = 3, MaxValue = 4 } },
                    },
                    Features = new List<Feature>
                    {
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "B", 3 },
                            }
                        },
                        new Feature
                        {
                            Name = Guid.NewGuid().ToString() ,
                            Properties = new Dictionary<string, int>
                            {
                                { "A", -2 },
                            }
                        }
                    }
                },
                2
            };
        }
    }
}