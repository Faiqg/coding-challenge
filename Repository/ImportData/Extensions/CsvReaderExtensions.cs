using CsvHelper;
using System;

namespace Repository.ImportData.Extensions
{
	public static class CsvReaderExtensions
	{
		public static string GetString(this CsvReader reader, int columnIndex)
		{
			return reader.GetField(columnIndex).Trim();
		}
		public static int? GetInt(this CsvReader reader, int columnIndex)
		{
			try
			{
				return int.Parse(reader.GetString(columnIndex));
			}
			catch { return null; }
		}
		public static double? GetDouble(this CsvReader reader, int columnIndex)
		{
			try
			{
				return Double.Parse(reader.GetString(columnIndex));
			}
			catch { return null; }
		}
		public static Decimal GetDecimal(this CsvReader reader, int columnIndex)
		{
			Decimal value;
			Decimal.TryParse(reader.GetString(columnIndex), out value);
			return value;
		}
		public static void SkipHeaders(this CsvReader reader, int count)
		{
			for (int i = 0; i < count; i++)
			{
				reader.Read();
				reader.ReadHeader();	
			}
		}
	}
}
