using Domain.DorZnakUA.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorZnakUA.DAL.Configuration;

public class RoadSignShieldConfiguration : IEntityTypeConfiguration<RoadSignShield>
{
    public void Configure(EntityTypeBuilder<RoadSignShield> builder)
    {
        builder.HasData(new List<RoadSignShield>()
        {
            new RoadSignShield()
            {
                RoadSignId = 1,
                ShieldId = 2,
            },
            new RoadSignShield()
            {
                RoadSignId = 2,
                ShieldId = 1,
            },
        });
    }
}