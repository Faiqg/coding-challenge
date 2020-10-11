using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.ImportData.Extensions
{
	public static class MathExtentions
	{
		public static decimal GetMedian(this List<int> source)
		{
			// Create a copy of the input, and sort the copy
			int[] temp = source.ToArray();
			Array.Sort(temp);
			int count = temp.Length;
			if (count == 0)
			{
				throw new InvalidOperationException("Empty collection");
			}
			else if (count % 2 == 0)
			{
				// count is even, average two middle elements
				int a = temp[count / 2 - 1];
				int b = temp[count / 2];
				return (a + b) / 2m;
			}
			else
			{
				// count is odd, return the middle element
				return temp[count / 2];
			}
		}
		public static int GetYear(this string input)
        {
			var y = System.Text.RegularExpressions.Regex.Match(input, @"\b(19|20)\d{2}\b");
			return Convert.ToInt32(y.Value);
		}
	}
}
