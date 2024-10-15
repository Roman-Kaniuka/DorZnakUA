using System.Text.RegularExpressions;
using Domain.DorZnakUA.Entity;
using Domain.DorZnakUA.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorZnakUA.DAL.Configuration;

public class ShieldConfiguration : IEntityTypeConfiguration<Shield>
{
    public void Configure(EntityTypeBuilder<Shield> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Group).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Shape).IsRequired().HasMaxLength(50);
        builder.Property(x => x.SizeType).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Height).IsRequired();
        builder.Property(x => x.Width).IsRequired();
        builder.Property(x => x.Weight).IsRequired();

        builder.HasMany(x => x.RoadSigns)
            .WithMany(x => x.Shields)
            .UsingEntity<RoadSignShield>(i => i
                    .HasOne<RoadSign>()
                    .WithMany()
                    .HasForeignKey(x => x.RoadSignId),
                i => i
                    .HasOne<Shield>()
                    .WithMany()
                    .HasForeignKey(x => x.ShieldId)
            );
        
        builder.HasData(new List<Shield>()
        {
            new Shield()
            {
                Id = 1,
                Group = nameof(ShieldGroups.WarningSigns),
                Name = "1.1",
                Shape = nameof(ShieldShapes.Triangle),
                SizeType = nameof(ShieldSizeTypes.I),
                Height = 0.6,
                Width = 0.7,
                Weight = 6.2,
            },
            new Shield()
            {
                Id = 2,
                Group = nameof(ShieldGroups.PrioritySigns),
                Name = "2.1",
                Shape = nameof(ShieldShapes.Triangle),
                SizeType = nameof(ShieldSizeTypes.I),
                Height = 0.6,
                Width = 0.7,
                Weight = 6.2,
            },
            new Shield()
            {
                Id = 3,
                Group = nameof(ShieldGroups.OrderSigns),
                Name = "4.1",
                Shape = nameof(ShieldShapes.Circle),
                SizeType = nameof(ShieldSizeTypes.I),
                Height = 0.6,
                Width = 0.6,
                Weight = 6.2,
            }
        });
    }
}