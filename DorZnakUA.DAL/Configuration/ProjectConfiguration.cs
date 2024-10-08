using Domain.DorZnakUA.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorZnakUA.DAL.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);

        builder.HasMany<RoadSign>(x => x.RoadSigns)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId)
            .HasPrincipalKey(x => x.Id);
        
        //тимчасовий код для заповнення таблці
        builder.HasData(new List<Project>()
        {
            new Project()
            {
                Id = 1,
                Name = "test Roma1",
                Description = "test1",
                UserId = 1,
                CreateAt = DateTime.UtcNow,
                CreateBy = 1,
                WindZoneId = 4,
            },
            new Project()
            {
                Id = 2,
                Name = "test Roma2",
                Description = "test2",
                UserId = 1,
                CreateAt = DateTime.UtcNow,
                CreateBy = 1,
                WindZoneId = 3,
            },
            new Project()
            {
                Id = 3,
                Name = "test Dima3",
                Description = "test3",
                UserId = 2,
                CreateAt = DateTime.UtcNow,
                CreateBy = 3,
                WindZoneId = 1,
            }

        });
    }
}