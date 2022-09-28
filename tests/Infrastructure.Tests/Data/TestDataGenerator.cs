using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace Infrastructure.Tests.Data
{
    public static class TestDataGenerator
    {
        public static IEnumerable<object[]> GetCurrentValue()
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
        
        public static IEnumerable<object[]> GetCurrentValueWithMax()
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
                0,
                3,
                false,
                9,
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
                0,
                3,
                false,
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
                0,
                3,
                false,
                1
            };

            yield return new object[]
            {
                new Character
                {
                    Name = Guid.NewGuid().ToString(),
                    Class = Guid.NewGuid().ToString(),
                    Attributes = new Dictionary<string, AttributeValue>
                    {
                        { "A", new AttributeValue { Value = 3, MaxValue = 10 } },
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
                2,
                5,
                true,
                10
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
                                { "B", 3 },
                            }
                        }
                    }
                },
                3,
                4,
                true,
                4
            };

            yield return new object[]
            {
                new Character
                {
                    Name = Guid.NewGuid().ToString(),
                    Class = Guid.NewGuid().ToString(),
                    Attributes = new Dictionary<string, AttributeValue>
                    {
                        { "A", new AttributeValue { Value = 3, MaxValue = 3 } },
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
                                { "A", -1 },
                            }
                        }
                    }
                },
                2,
                3,
                true,
                3
            };
        }

        public static IEnumerable<object[]> GetCurrentMaxValue()
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