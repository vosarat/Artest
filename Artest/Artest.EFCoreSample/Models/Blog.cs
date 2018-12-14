using System;
using System.Collections.Generic;
using System.Text;

namespace Artest.EFCoreSample.Models
{
	public class Blog
	{
		public int Id { get; set; }
		public string Url { get; set; }

		public List<Post> Posts { get; set; }
	}
}
