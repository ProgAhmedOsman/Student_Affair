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
    public class StudentMap
    {
        public StudentMap(EntityTypeBuilder<Student> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Key);

            entityBuilder.HasOne(x => x.ClassRoom).WithMany(x => x.Students).HasForeignKey(x => x.ClassRoom_key);
            entityBuilder.HasMany(x => x.Subjects).WithOne(x => x.Student).IsRequired().HasForeignKey(x => x.Student_Key).OnDelete(DeleteBehavior.Cascade);
            entityBuilder.Property(x => x.CreateDate).HasColumnType("datetime2").HasPrecision(7);
            entityBuilder.Property(x => x.ModifiedDate).HasColumnType("datetime2").HasPrecision(7);
            entityBuilder.Property(e => e.BirthDate).HasColumnType("date");

        }

    }
}
 
 
