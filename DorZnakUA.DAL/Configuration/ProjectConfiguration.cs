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
                CreateBy = 1
            },
            new Project()
            {
                Id = 2,
                Name = "test Roma2",
                Description = "test2",
                UserId = 1,
                CreateAt = DateTime.UtcNow,
                CreateBy = 1
            },
            new Project()
            {
                Id = 3,
                Name = "test Dima3",
                Description = "test3",
                UserId = 2,
                CreateAt = DateTime.UtcNow,
                CreateBy = 3
            }

        });
    }
}