using System.Collections;
using System.Globalization;
using UBS.Risk.Common;

namespace UBS.Risk.DataAccess;

/// <summary>
/// Represents a stream of trades as an iterator of <see cref="Trade"/>.
/// </summary>
public sealed class TradesStream : IEnumerable<Trade>
{
	private readonly StreamReader _reader;
	private int _totalLines;

	/// <summary>
	/// Initializes a new instance of the <see cref="TradesStream"/> class with the specified reader.
	/// </summary>
	/// <param name="reader">A StreamReader that starts on the first Trade line.</param>
	/// <param name="totalLines">Total number of lines to be read from the reader.</param>
	public TradesStream(StreamReader reader, int totalLines)
	{
		_reader = reader;
		_totalLines = totalLines;
	}

	/// <summary>
	/// Returns an enumertor of trades.
	/// </summary>
	/// <returns>An IEnumerator object of trades.</returns>
	public IEnumerator<Trade> GetEnumerator()
	{
		string? line;
		while (_totalLines > 0 && (line = _reader.ReadLine()) != null)
		{
			yield return TradeParser.Parse(line);
			_totalLines--;
		}
	}

	/// <summary>
	/// Returns an enumertor of trades.
	/// </summary>
	/// <returns>An IEnumerator object of trades.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	
	private static class TradeParser
	{
		/// <summary>
		/// Returns a trade from the specified line.
		/// A trade line is composed of three parts separated by a space, for example:
		/// "1000 Public 01/01/2022"
		/// </summary>
		/// <param name="line">String containing a trade</param>
		/// <returns>A <see cref="Trade" /></returns>
		/// <exception cref="FormatException">If the line doesn't have all three parts or the date isn't formatted properly</exception>
		/// <exception cref="ArgumentNullException">If any of the parts is empty</exception>
		public static Trade Parse(string line)
		{
			var parts = line.Split(' ');
			if (parts.Length != 3)
			{
				throw new FormatException($"Invalid line: '{line}'");
			}

			var value = double.Parse(parts[0]);
			var clientSector = parts[1];
			var nextPaymentDate = DateTime.ParseExact(parts[2], "d", CultureInfo.InvariantCulture);

			return new Trade(value, clientSector, nextPaymentDate);
		}
	}
}