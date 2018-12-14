using System;
using System.Collections.Generic;
using System.Text;

using Artest.EFCoreSample.Models;

using Microsoft.EntityFrameworkCore;

namespace Artest.EFCoreSample
{
	public class BloggingContext : DbContext
	{
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }

		public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
		{		
		}
	}
}
