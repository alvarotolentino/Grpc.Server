using System;
using System.Collections.Generic;
using Grpctest.Messages;

namespace Grpctest
{
    public static class Users
    {
        public static List<User> users
        {
            get
            {
                var u1 = new User
                {
                    Id = 1,
                    FirstName = "A",
                    LastName = "T",
                    Birthday = new DateTime(1981, 3, 15).Ticks
                };

                u1.Vehicles.Add(new Vehicle
                {
                    Id = 1,
                    RegNumber = "HI123123"
                });

                u1.Vehicles.Add(new Vehicle
                {
                    Id = 2,
                    RegNumber = "JI123123"
                });

                var u2 = new User
                {
                    Id = 2,
                    FirstName = "J",
                    LastName = "T",
                    Birthday = new DateTime(1983, 3, 5).Ticks
                };

                u2.Vehicles.Add(new Vehicle
                {
                    Id = 3,
                    RegNumber = "KI"
                });

                u2.Vehicles.Add(new Vehicle
                {
                    Id = 4,
                    RegNumber = "LI"
                });

                var u3 = new User
                {
                    Id = 3,
                    FirstName = "S",
                    LastName = "T",
                    Birthday = new DateTime(1984, 3, 28).Ticks
                };

                u3.Vehicles.Add(new Vehicle
                {
                    Id = 5,
                    RegNumber = "PI"
                });

                u3.Vehicles.Add(new Vehicle
                {
                    Id = 6,
                    RegNumber = "NI"
                });

                return new List<User>{
                    u1, u2, u3
                };
            }
        }
    }
}