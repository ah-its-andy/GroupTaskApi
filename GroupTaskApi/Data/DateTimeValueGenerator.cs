using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace GroupTaskApi.Data
{
    public class DateTimeValueGenerator : ValueGenerator<DateTime>
    {
        public override DateTime Next(EntityEntry entry)
            => DateTime.Now;

        public override bool GeneratesTemporaryValues => false;
    }
}
