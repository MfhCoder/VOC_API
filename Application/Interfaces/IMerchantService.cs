using Application.Dtos.UserDtos;
using Application.DTOs;
using Application.Specifications;
using Core.Entities;
using System.Text;

namespace Application.Interfaces;

public interface IMerchantService
{
    public StringBuilder GenerateCsv(IReadOnlyList<Merchant> merchants);
    public void ParseTenure(MerchantSpecParams specParams);
    public Task<byte[]> ExportMerchantsCSV(MerchantSpecParams specParams);
    public Task<byte[]> ExportMerchantsPDF(MerchantSpecParams specParams);

}

