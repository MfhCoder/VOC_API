using Application.Interfaces;
using Application.Specifications;
using AutoMapper;
using Core.Entities;
using Core.Helpers;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IUnitOfWork unit;
        public MerchantService(IUnitOfWork unitOfWork)
        {
            unit = unitOfWork;
        }

        public StringBuilder GenerateCsv(IReadOnlyList<Merchant> merchants)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Industry,Location,Tenure,TenureInDays,PhoneNo,LastSurvey,LastFeedback,Ledger,LastTransaction,LastTicket,CreatedOn,LastEscalation");

            foreach (var m in merchants)
            {
                csv.AppendLine($"{m.Id},{CsvHelper.EscapeCsv(m.Name)},{CsvHelper.EscapeCsv(m.Industry)},{CsvHelper.EscapeCsv(m.Location)},{CsvHelper.EscapeCsv(m.Tenure)},{m.TenureInDays},{CsvHelper.EscapeCsv(m.PhoneNo)},{CsvHelper.EscapeCsv(m.LastSurvey?.ToString())},{CsvHelper.EscapeCsv(m.LastFeedback?.ToString())},{CsvHelper.EscapeCsv(m.Ledger?.ToString())},{CsvHelper.EscapeCsv(m.LastTransaction?.ToString())},{CsvHelper.EscapeCsv(m.LastTicket?.ToString())},{CsvHelper.EscapeCsv(m.CreatedOn.ToString())},{CsvHelper.EscapeCsv(m.LastEscalation?.ToString())}");
            }

            return csv;
        }

        public void ParseTenure(MerchantSpecParams specParams)
        {
            if (!string.IsNullOrEmpty(specParams.MinTenure))
                specParams.MinTenureInDays = TenureHelper.ParseTenureToDays(specParams.MinTenure);

            if (!string.IsNullOrEmpty(specParams.MaxTenure))
                specParams.MaxTenureInDays = TenureHelper.ParseTenureToDays(specParams.MaxTenure);
        }

        public async Task<byte[]> ExportMerchantsCSV(MerchantSpecParams specParams)
        {
            ParseTenure(specParams);

            var spec = new MerchantSpecification(specParams);
            var merchants = await unit.Repository<Merchant>().ListAsync(spec);
            var csv = GenerateCsv(merchants);

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return bytes;
        }

        public async Task<byte[]> ExportMerchantsPDF(MerchantSpecParams specParams)
        {
            ParseTenure(specParams);

            var spec = new MerchantSpecification(specParams);
            var merchants = await unit.Repository<Merchant>().ListAsync(spec);
            var document = new MerchantsPdfDocument(merchants);
            var pdfBytes = document.GeneratePdf();

            return pdfBytes;
        }
    }
} 