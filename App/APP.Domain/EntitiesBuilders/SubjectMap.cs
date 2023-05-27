using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace APP.Domain.EntitiesBuilders
{
    public class SubjectMap
    {
        public SubjectMap(EntityTypeBuilder<Subject> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Key);
            entityBuilder.HasMany(x => x.Subjects).WithOne(x => x.Subject).IsRequired().HasForeignKey(x => x.Subject_Key).OnDelete(DeleteBehavior.Cascade);
            entityBuilder.Property(x => x.CreateDate).HasColumnType("datetime2").HasPrecision(7);
            entityBuilder.Property(x => x.ModifiedDate).HasColumnType("datetime2").HasPrecision(7);


        }

    }
}


