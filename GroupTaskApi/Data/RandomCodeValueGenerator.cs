using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace GroupTaskApi.Data
{
    public class RandomCodeValueGenerator : ValueGenerator<string>
    {
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();
        public override string Next(EntityEntry entry)
        {
            var data = new byte[32];
            RandomNumberGenerator.Fill(data);
            return BitConverter.ToString(data).Replace("-", "").Substring(0, 4).ToUpper();
        }

        public override bool GeneratesTemporaryValues => false;
    }
}
