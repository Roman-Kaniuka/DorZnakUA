using Domain.DorZnakUA.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorZnakUA.DAL.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Login).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(50);

        builder.HasMany<Project>(x => x.Projects)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .HasPrincipalKey(x => x.Id);
        
        //тимчасовий код для заповнення таблці
        builder.HasData(new List<User>()
        {
            new User()
            {
                Id = 1,
                Login = "Roma",
                Password = "qwerty",
                CreateAt = DateTime.UtcNow
            },
            new User()
            {
                Id = 2,
                Login = "Dima",
                Password = "kamykadze",
                CreateAt = DateTime.UtcNow
            },
        });
    }
}