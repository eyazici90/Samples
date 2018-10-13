
using CustomerSample.Common.Dtos;
using System.Threading.Tasks;

namespace CustomerSample.Application.Abstractions
{
    public interface ICustomerAppService 
    {
        Task<BrandDto> GetBrandByIdAsync(int brandId);
        Task<int> AddNewBrand(BrandDto brandDto);
        Task<int> AddMerchantToBrand(MerchantDto merchant);
        Task<int> ChangeBrandName(BrandDto brandDto);
        Task<int> ChangeMerchantVknByBrand(MerchantDto merchant);
    }
}
