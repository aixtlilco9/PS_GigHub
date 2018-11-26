using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using GigHub.Core.Models;

namespace GigHub.Persistence.EntityConfigurations
{
    public class GigConfig :EntityTypeConfiguration<Gig>
    {//key, property, hasmany
        public GigConfig()
        {
            //do not need modelbuilder.Entity<>(). as EntitytypeConfig has property inside as a method that we inherit
           Property(g => g.ArtistId)
                .IsRequired();

           Property(g => g.GenreId)
                        .IsRequired();

            Property(g => g.Venue)
                .IsRequired()
                .HasMaxLength(255);
 
                HasMany(g => g.Attendances)
                .WithRequired(a => a.Gig)
                .WillCascadeOnDelete(false);

        }


    }
}