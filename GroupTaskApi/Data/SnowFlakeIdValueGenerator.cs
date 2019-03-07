using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Snowflake.Core;

namespace GroupTaskApi.Data
{
    public class SnowFlakeIdValueGenerator : ValueGenerator<long>
    {
        private readonly IdWorker _idWorker = new IdWorker(1,1);
        
        public override long Next(EntityEntry entry)
            => _idWorker.NextId();

        public override bool GeneratesTemporaryValues => false;
    }
}
