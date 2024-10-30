using System.Globalization;

namespace UBS.Risk.DataAccess;

/// <summary>
/// Represents a reader for the trades file.
/// </summary>
public class TradesFileReader : IDisposable
{
    private readonly StreamReader _reader;
    private int _currentLine = 0;
    private int _totalLines = 0;
    private DateTime _referenceDate;

    /// <summary>
    /// Initializes a new instance of the <see cref="TradesFileReader"/> class with the specified file name.
    /// </summary>
    /// <param name="fileName">The name of the input file.</param>
    /// <exception cref="ArgumentException">fileName does not exist.</exception>
    public TradesFileReader(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new ArgumentException($"File '{fileName}' does not exist");
        }

        _reader = new StreamReader(fileName);
    }

    /// <summary>
    /// Returns the reference date from the file.
    /// </summary>
    /// <returns>The reference date.</returns>
    /// <exception cref="FormatException">If the reference date format is not MM/dd/yyyy</exception>
    public DateTime GetReferenceDate()
    {
        ProcessHeader();

        return _referenceDate;
    }

    /// <summary>
    /// Returns a <see cref="TradesStream"/> of trades from the file.
    /// </summary>
    /// <returns>An iterable object containing trades.</returns>
    /// <exception cref="FormatException">If it's not possible to parse the header correctly.</exception>
    public TradesStream GetTrades()
    {
        ProcessHeader();

        return new TradesStream(_reader, _totalLines);
    }
    
    // Process the header part of the file that contains the reference date and the total lines.
    // The reference date format is MM/dd/yyyy.
    private void ProcessHeader()
    {
        if (_currentLine == 0)
        {
            string? line = _reader.ReadLine();
            if (line == null)
            {
                throw new FormatException("Expecting a line with the reference date");
            }

            _referenceDate = DateTime.ParseExact(line, "d", CultureInfo.InvariantCulture);
            _currentLine++;
        }

        if (_currentLine == 1)
        {
            string? line = _reader.ReadLine();
            if (line == null)
            {
                throw new FormatException("Expecting a line with the total lines");
            }
            _totalLines = int.Parse(line);
            _currentLine++;
        }
    }

    public void Dispose() => _reader.Dispose();
}