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
    public class StudentSubjectMap
    {
        public StudentSubjectMap(EntityTypeBuilder<StudentSubject> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Key);
            entityBuilder.Property(x => x.CreateDate).HasColumnType("datetime2").HasPrecision(7);
            entityBuilder.Property(x => x.ModifiedDate).HasColumnType("datetime2").HasPrecision(7);
        }

    }
}


