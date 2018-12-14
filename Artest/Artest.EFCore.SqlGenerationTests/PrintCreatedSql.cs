using System;
using System.Linq;

using Artest.EFCoreSample;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#pragma warning disable 618

namespace Artest.EFCore.SqlGenerationTests
{
	[TestClass]
	public class PrintCreatedSql
	{
		private LoggerFactory loggerFactory = new LoggerFactory(new[]
		                                                        {
				                                                        new ConsoleLoggerProvider((category, level) =>
						                                                                                  category == DbLoggerCategory.Database.Command.Name
						                                                                                  && level == LogLevel.Debug, true)
		                                                        });

		[TestMethod]
		public void Case1()
		{
			var connection = new SqliteConnection("DataSource=:memory:");
			connection.Open();

			try
			{
				var options = new DbContextOptionsBuilder<BloggingContext>()
				              .UseSqlite(connection)
				              .Options;

				using (var context = new BloggingContext(options))
				{
					context.Database.EnsureCreated();
				}

				options = new DbContextOptionsBuilder<BloggingContext>()
				              .UseSqlite(connection)
				              .UseLoggerFactory(this.loggerFactory)
				              .Options;

				using (var context = new BloggingContext(options))
				{
					var selectQuery = from b in context.Blogs
					                  join p in context.Posts on b.Id equals p.BlogId into ps from p in ps.DefaultIfEmpty()
					                  select new { b.Url, Titul = p.Title };


					var posts = selectQuery.Where(d => d.Url == "test url" && d.Titul == "test title").ToList();
					Assert.AreEqual(0, posts.Count);
				}
			}
			finally
			{
				connection.Close();
			}
		}
	}
}
