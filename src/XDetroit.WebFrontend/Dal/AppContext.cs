using XDetroit.WebFrontend.Models;

namespace XDetroit.WebFrontend.Dal
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AppContext : DbContext
    {
        // Your context has been configured to use a 'AppContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'XDetroit.WebFrontend.Dal.AppContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AppContext' 
        // connection string in the application configuration file.
        public AppContext()
            : base("name=AppContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppContext>()); //To recreate the database when it doesn't match the model.
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<ItemCategory> Categories { get; set; }
    }
}