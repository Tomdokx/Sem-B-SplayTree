using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_B_st60982.SplayTree
{
	public class TestProduct : IComparable<TestProduct>
	{
		public int ID { get; init; } = Generator.ProductID;
		public string ProductServiceNumber { get; set; } = Generator.GetNewProductNumber();

		public int CompareTo(TestProduct? other)
		{
			return ProductServiceNumber.CompareTo(other!.ProductServiceNumber);
		}
	}
}
