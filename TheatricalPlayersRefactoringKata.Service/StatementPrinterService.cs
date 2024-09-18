using FluentValidation;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Models.Dtl;
using TheatricalPlayersRefactoringKata.Domain.Repositories;
using TheatricalPlayersRefactoringKata.Service.Interfaces;
using System.Text;
using System;

namespace TheatricalPlayersRefactoringKata.Service
{
    public class StatementPrinterService : IStatementPrinterService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPlayRepository _playRepository;

        public StatementPrinterService( IInvoiceRepository invoiceRepository, IPlayRepository playRepository )
        {
            _invoiceRepository = invoiceRepository;
            _playRepository = playRepository;
        }

        public async Task<string> GenerateXmlStatementAsync(int invoiceId)
        {
            try
            {
                var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);

                if (invoice == null)
                {
                    throw new ArgumentNullException("Invoice not found");
                }

                var statement = new StatementXmlDto
                {
                    Customer = invoice.Customer,
                    Items = new List<StatementXmlItemDto>(),
                    AmountOwed = 0,
                    EarnedCredits = 0
                };

                foreach (var perf in invoice.Performances)
                {
                    var play = await _playRepository.GetPlayBySlugAsync(perf.FkPlaySlug);
                    if (play == null) throw new ArgumentNullException("Play not found");

                    decimal amount = CalculateAmount(play, perf);
                    var volumeCredits = CalculateVolumeCredits(play, perf);

                    statement.Items.Add(new StatementXmlItemDto
                    {
                        AmountOwed = amount / 100,
                        EarnedCredits = volumeCredits,
                        Seats = perf.Audience
                    });

                    statement.AmountOwed += amount / 100;
                    statement.EarnedCredits += volumeCredits;
                }

                var xml = SerializeToXml(statement);
                return xml;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> PrintStatementAsync(int invoiceId)
        {
            try
            {
                var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);

                if (invoice == null)
                {
                    throw new ArgumentNullException("Invoice not found");
                }

                decimal totalAmount = 0;
                var volumeCredits = 0;
                var result = string.Format("Statement for {0}\n", invoice.Customer);

                var cultureInfo = new CultureInfo("en-US");

                foreach (var perf in invoice.Performances)
                {
                    var play = await _playRepository.GetPlayBySlugAsync(perf.FkPlaySlug);
                    if (play == null) throw new ArgumentNullException("Play not found");

                    decimal thisAmount = CalculateAmount(play, perf);
                    volumeCredits += CalculateVolumeCredits(play, perf);

                    result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                    totalAmount += thisAmount;
                }

                result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
                result += String.Format("You earned {0} credits\n", volumeCredits);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private decimal CalculateAmount(PlayEntity play, PerformanceEntity perf)
        {
            var lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            int thisAmount = lines * 10;

            switch (play.Type)
            {
                case PlayType.Tragedy:
                    thisAmount = CalculateTragedyPrice(perf.Audience, thisAmount);
                 break;

                case PlayType.Comedy:
                    thisAmount = CalculateComedyPrice(perf.Audience, thisAmount); 
                break;

                case PlayType.History:
                    thisAmount = CalculateTragedyPrice(perf.Audience, thisAmount) + CalculateComedyPrice(perf.Audience, thisAmount); 
                break;
                
                default: 
                    throw new Exception("unknown type: " + play.Type);
            }

            return thisAmount;
        }

        private int CalculateVolumeCredits(PlayEntity play, PerformanceEntity perf)
        {
            int volumeCredits = Math.Max(perf.Audience - 30, 0);
            if (play.Type == PlayType.Comedy)
            {
                volumeCredits += perf.Audience / 5;
            }

            return volumeCredits;
        }

        private int CalculateTragedyPrice(int audience, int amount)
        {
            if(audience > 30)
            {
                amount += 1000 * (audience - 30);
            }

            return amount;
        }

        private int CalculateComedyPrice(int audience, int amount)
        {
            if (audience > 20)
            {
                amount += 10000 + 500 * (audience - 20);
            }
            amount += 300 * audience;

            return amount;
        }

        private string SerializeToXml(StatementXmlDto statement)
        {
            var xmlSerializer = new XmlSerializer(typeof(StatementXmlDto));

            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    using (var xmlWriter = XmlWriter.Create(streamWriter, xmlSettings))
                    {
                        xmlSerializer.Serialize(xmlWriter, statement);
                        streamWriter.Flush(); // Garante que todos os dados sejam gravados no MemoryStream
                        return Encoding.UTF8.GetString(memoryStream.ToArray()); // Converte os bytes do MemoryStream para string
                    }
                }
            }
        }


    }
}
