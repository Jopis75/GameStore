using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company
                {
                    Id = 1,
                    CompanyType = CompanyType.Division,
                    Name = "PlayStation Studios",
                    TradeName = "PlayStation Studios",
                    Description = "PlayStation Studios (formerly SCE Worldwide Studios and SIE Worldwide Studios) is a division of Sony Interactive Entertainment (SIE) that oversees the video game development at the studios owned by SIE. The division was established as SCE Worldwide Studios in September 2005 and rebranded as PlayStation Studios in 2020.",
                    Founded = DateTime.Parse("2005-09-14"),
                    HeadquartersId = 1,
                    NumberOfEmployees = 4000,
                    ParentCompanyId = null,
                    WebsiteUrl = "https://www.playstation.com/en-us/corporate/playstation-studios/",
                    LogoImageUri = string.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                },
                new Company
                {
                    Id = 2,
                    CompanyType = CompanyType.Subsidiary,
                    Name = "Guerrilla",
                    TradeName = "Guerrilla Games",
                    Description = "Guerrilla B.V. (trade name: Guerrilla Games) is a Dutch first-party video game developer based in Amsterdam and part of PlayStation Studios. The company was founded as Lost Boys Games in January 2000 through the merger of three smaller development studios as a subsidiary of multimedia conglomerate company Lost Boys. Lost Boys Games became independent the following year and was acquired by Media Republic in 2003, renaming the studio to Guerrilla Games before being purchased by Sony Interactive Entertainment in 2005. As of June 2021, the company employs 360 people[1] under the leadership of joint studio directors Jan-Bart van Beek, Joel Eschler and Hella Schmidt. It is best known for the Killzone and Horizon game series.",
                    Founded = DateTime.Parse("2000-01-01"),
                    HeadquartersId = 2,
                    NumberOfEmployees = 360,
                    ParentCompanyId = 1,
                    WebsiteUrl = "https://www.guerrilla-games.com/",
                    LogoImageUri = string.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                },
                new Company
                {
                    Id = 3,
                    CompanyType = CompanyType.Subsidiary,
                    Name = "Sucker Punch",
                    TradeName = "Sucker Punch Productions",
                    Description = "Sucker Punch Productions, LLC is an American video game developer based in Bellevue, Washington. It is best known for creating character action games for PlayStation consoles such as Sly Cooper, Infamous, and Ghost of Tsushima. The studio has been a part of PlayStation Studios since 2011. As of 2020, the company employs about 160 people.[1]\r\n\r\nSucker Punch Productions was founded in October 1997 by Brian Fleming, Bruce Oberg, Darrell Plank, Tom and Cathy Saxton, and Chris Zimmerman. The founders worked at Microsoft before joining the video game industry. Despite having a difficult time finding a publisher and funding, the group's first project, Rocket: Robot on Wheels, was released in 1999. While it did not perform well commercially, it was well received by critics, which encouraged the team to develop another platform game in 2002 named Sly Cooper and the Thievius Raccoonus. The team approached Sony Computer Entertainment to publish the game, which became an unexpected commercial success and spawned a franchise with two sequels: Sly 2: Band of Thieves (2004) and Sly 3: Honor Among Thieves (2005).\r\n\r\nAfter working on three Sly Cooper games, the team continued its partnership with Sony and pivoted to making an open-world, comic book-inspired superhero game titled Infamous (2009). Infamous was a modest success and Sucker Punch followed it up with two sequels, Infamous 2 (2011) and Infamous Second Son (2014). After the release of Infamous 2, Sony acquired Sucker Punch for an undisclosed sum. Following the release of Second Son, the studio spent six years working on the next game, Ghost of Tsushima (2020), which went on to become one of Sony's fastest-selling original games for the PlayStation 4, selling more than 9.73 million copies.",
                    Founded = DateTime.Parse("1997-10-01"),
                    HeadquartersId = 3,
                    NumberOfEmployees = 160,
                    ParentCompanyId = 1,
                    WebsiteUrl = "https://www.suckerpunch.com/",
                    LogoImageUri = string.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                }
            );
        }
    }
}
