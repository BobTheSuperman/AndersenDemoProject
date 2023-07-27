using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new ApiClientEntityConfiguration())
            //.ApplyConfiguration(new ApiClientFilterEntityConfiguration())
            //.ApplyConfiguration(new BookmakerEntityConfiguration())
            //.ApplyConfiguration(new CurrencyEntityConfiguration())
            //.ApplyConfiguration(new DefinitionEntityConfiguration())
            //.ApplyConfiguration(new DefinitionTypeEntityConfiguration())
            //.ApplyConfiguration(new UserBetStrategyEntityConfiguration())
            //.ApplyConfiguration(new UserProxyEntityConfiguration())
            //.ApplyConfiguration(new UsersBetEntityConfiguration())
            //.ApplyConfiguration(new UsersBookmakerEntityConfiguration())
            //.ApplyConfiguration(new UsersCaptchaServiceEntityConfiguration())
            //.ApplyConfiguration(new UsersFilterBookmakerEntityConfiguration())
            //.ApplyConfiguration(new UsersFilterCountryEntityConfiguration())
            //.ApplyConfiguration(new UsersFilterEntityConfiguration())
            //.ApplyConfiguration(new UsersFilterLeagueEntityConfiguration())
            //.ApplyConfiguration(new UsersSubscriptionConfiguration())
            //.ApplyConfiguration(new UsersTaskEntityConfiguration())
            //.ApplyConfiguration(new CountryEntityConfiguration())
            //.ApplyConfiguration(new LanguageEntityConfiguration())
            //.ApplyConfiguration(new CountryLanguageEntityConfiguration())
            //.ApplyConfiguration(new BookmakerNamesMatchingEntityConfiguration())
            //.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}
