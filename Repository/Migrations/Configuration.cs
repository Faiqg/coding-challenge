using CsvHelper;
using Data;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Repository.Migrations
{
	public class Configuration : DbMigrationsConfiguration<LGAContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}
	}
}
