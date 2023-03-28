using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_B_st60982.SplayTree
{
	public static class Generator
	{
		private static int _productId = 0;
		public static int ProductID { get { return _productId++; } }

		public static void Reset()
		{
			_productId = 0;
		}

		public static string GetNewProductNumber()
		{
			Random rand = new Random();

			// Set up the characters that can be used in the string
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

			// Generate a random string of 8 characters
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 5; i++)
			{
				sb.Append(chars[rand.Next(chars.Length)]);
			}
			sb.Append("-");
			for (int i = 0; i < 5; i++)
			{
				sb.Append(chars[rand.Next(chars.Length)]);
			}
			string randomString = sb.ToString();
			return randomString;
		}
	}
}
