using UBS.Risk.Categorizer;
using UBS.Risk.Categorizer.Strategies;
using UBS.Risk.Common;
using UBS.Risk.DataAccess;

namespace UBS.Risk.Cli;

class Program
{
    static void Main(string[] args)
    {
        TradeCategorizer tradeCategorizer = new([
            new ExpiredTradeCategorizer(),
            new MediumRiskCategorizer(),
            new LowRiskCategorizer()
        ]);

        try
        {
            CommandLineArguments arguments = ParseArgs(args);
            TradesFileReader reader = new(arguments.FileName);

            using StreamWriter writer = GetWriter(arguments);

            TradeCategorizerParameters parameters = new() { ReferenceDate = reader.GetReferenceDate() };
            foreach (Trade trade in reader.GetTrades())
            {
                TradeCategory category = tradeCategorizer.Categorize(trade, parameters);
                writer.WriteLine(category);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static StreamWriter GetWriter(CommandLineArguments arguments)
    {
        StreamWriter? writer;
        if (arguments.OutputFileName != "")
        {
            writer = new(arguments.OutputFileName);
        }
        else
        {
            writer = new(Console.OpenStandardOutput());
        }
        writer.AutoFlush = true;
        
        return writer;
    }

    static CommandLineArguments ParseArgs(string[] args)
    {
        if (args.Length < 1)
        {
            throw new ArgumentException("File name is required\nUsage: UBS.Risk.Cli <file name> [<output file name>]");
        }

        var result = new CommandLineArguments
        {
            FileName = args[0]
        };

        if (args.Length > 1)
        {
            result.OutputFileName = args[1];
        }

        return result;
    }

    class CommandLineArguments
    {
        public string FileName { get; set; } = "";
        public string OutputFileName { get; set; } = "";
    }
}