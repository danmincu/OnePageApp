using System.Collections.Generic;

namespace OnePageApp.Services.Examples
{
    public class PermissionsServiceExample : IPermissions
    {
        public IEnumerable<BasePermission> GetPermissionTree()
        {
            return new[]
            {
                new Portofolio
                {
                    Name = "NA",
                    Code = 1,
                    Items = new []
                    {
                        new Branch
                        {
                            Name = "USA",
                            Code = 1,
                            Items = new []
                            {
                                new Unit
                                {
                                    Name = "NY",
                                    Code = 1
                                },
                                new Unit
                                {
                                    Name = "Texas",
                                    Code = 2
                                },
                                new Unit
                                {
                                    Name = "California",
                                    Code = 3
                                },
                                new Unit
                                {
                                    Name = "Maine",
                                    Code = 4
                                },
                                new Unit
                                {
                                    Name = "Vermont",
                                    Code = 5
                                }

                            }
                        },
                        new Branch
                        {
                            Name = "Canada",
                            Code = 2,
                            Items = new []
                            {
                                new Unit
                                {
                                    Name = "Ontario",
                                    Code = 1
                                },
                                new Unit
                                {
                                    Name = "BC",
                                    Code = 2
                                },
                                new Unit
                                {
                                    Name = "Alberta",
                                    Code = 3
                                }

                            }
                        },
                        new Branch
                        {
                            Name = "Mexico",
                            Code = 3,
                            Items = new []
                            {
                                new Unit
                                {
                                    Name = "Quintana Roo",
                                    Code = 1
                                },
                                new Unit
                                {
                                    Name = "Chiuaua",
                                    Code = 2
                                }
                            }
                        }
                    }
                },
                new Portofolio
                {
                    Name = "Europe",
                    Code = 2,
                    Items = new []
                    {
                        new Branch
                        {
                            Name = "Germany",
                            Code = 1,
                            Items = new []
                            {
                                new Unit
                                {
                                    Name = "Bavaria",
                                    Code = 1
                                },
                                new Unit
                                {
                                    Name = "Berlin",
                                    Code = 2
                                },
                                new Unit
                                {
                                    Name = "Saxony",
                                    Code = 3
                                },
                                new Unit
                                {
                                    Name = "Hesse",
                                    Code = 4
                                },
                                new Unit
                                {
                                    Name = "Saarland",
                                    Code = 5
                                }

                            }
                        },
                        new Branch
                        {
                            Name = "UK",
                            Code = 2,
                            Items = new []
                            {
                                new Unit
                                {
                                    Name = "England",
                                    Code = 1
                                },
                                new Unit
                                {
                                    Name = "Wales",
                                    Code = 2
                                },
                                new Unit
                                {
                                    Name = "Scotland",
                                    Code = 3
                                },
                                new Unit
                                {
                                    Name = "Northen Ireland",
                                    Code = 4
                                }
                            }
                        },
                    }
                },
                new Portofolio
                {
                    Name = "Asia",
                    Code = 3,
                    Items = new []
                    {
                        new Branch
                        {
                            Name = "China",
                            Code = 1,
                            Items = new []
                            {
                                new Unit
                                {
                                    Name = "Sichuan",
                                    Code = 1
                                },
                                new Unit
                                {
                                    Name = "Guangdong",
                                    Code = 2
                                },
                                new Unit
                                {
                                    Name = "Fujian",
                                    Code = 3
                                },
                                new Unit
                                {
                                    Name = "Hainan",
                                    Code = 4
                                },
                                new Unit
                                {
                                    Name = "Hebei",
                                    Code = 5
                                }
                            }
                        },
                    }
                }
            };
        }
    }
}