using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MinkyShopProject.Common
{
	public static class Helper
	{
		public static ActionResult TransformData(Response data)
		{
			var result = new ObjectResult(data) { StatusCode = (int)data.Code };
			return result;
		}

		public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
		{
			IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };
			foreach (var sequence in sequences)
			{
				var localSequence = sequence;
				result = result.SelectMany(
				  _ => localSequence,
				  (seq, item) => seq.Concat(new[] { item })
				);
			}
			return result;
		}

		public static double CalculateChange(long previous, long current)
		{
			if (previous == 0)
				throw new InvalidOperationException();

			var change = current - previous;
			return (double)change / previous;
		}

		public static string DoubleToPercentageString(double d)
		{
			return (Math.Round(d, 1) * 100) + "%";
		}

		public static T NewObject<T>(this T obj)
		{
			return obj;
		}

		public static string RemoveSpecialCharacters(this string str)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in str)
			{
				if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

		public static bool CheckNumber(this string str)
		{
			Regex regex = new Regex(@"^[0-9.]+$");
			return regex.IsMatch(str);
		}

		public static string BaCham(this string text, int soLuong)
		{
			return soLuong < text.Length ? text.Substring(0, soLuong) + "..." : text;
		}

		public static T ConvertJson<T>(object obj)
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
		}

		public static string ToTitleCase(this string title)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower().Trim());
		}

		public static string RandomString(int length)
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static string RandomNumber(int length)
		{
			Random random = new Random();
			const string chars = "0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static bool CheckCharacterSpecial(this string value)
		{
			return value.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c) ? true : false);
		}

		public static string FormatMoney(float? money)
		{
			return string.Format("{0:0,0}" + " ₫", money);
		}

		public static string LowerText(string text)
		{
			return text.ToLower().Trim();
		}
	}
}
