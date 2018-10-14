
using CustomerSample.Application.Abstractions;
using CustomerSample.Common.Dtos;
using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using Galaxy.Infrastructure;
using Galaxy.ObjectMapping;
using Galaxy.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSample.Application
{
   public class CustomerAppService : ICustomerAppService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IBrandPolicy _brandPolicy;
        private readonly IUnitOfWorkAsync _unit;
        private readonly IObjectMapper _objectMapper;
        public CustomerAppService(IBrandRepository brandRepository
            , IBrandPolicy brandPolicy
            , IUnitOfWorkAsync unit
            , IObjectMapper objectMapper)
        {
            this._brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            this._brandPolicy = brandPolicy ?? throw new ArgumentNullException(nameof(brandPolicy));
            this._unit = unit ?? throw new ArgumentNullException(nameof(unit));
            this._objectMapper = objectMapper ?? throw new ArgumentNullException(nameof(objectMapper));
        }

        public async Task<IList<BrandDto>> GetAllBrandsAsync()
        {
            return this._objectMapper.MapTo<IList<BrandDto>>(
                    await this._brandRepository.GetAllBrands().ToListAsync()
            );
        }
        public async Task AddNewBrand(BrandDto brandDto)
        {
            // Authorization for application should be in this layer or caching !!! Any infrastructure knowing things
            var brand = Brand.Create(brandDto.EMail, brandDto.BrandName, brandDto.Gsm, brandDto.SNCode);
            this._brandRepository.Add(brand);
        }

        public async Task<BrandDto> GetBrandByIdAsync(int brandId)
        {
            var brand = await this._brandRepository.GetAsync(brandId);
            return this._objectMapper.MapTo<BrandDto>(brand);
        }


        public async Task AddMerchantToBrand(MerchantDto merchant)
        {
            var brand = await this._brandRepository.GetAsync(merchant.BrandId);
            brand
                .AddMerchant(merchant.Name, merchant.Surname, merchant.BrandId, merchant.LimitId, _brandPolicy, merchant.Gsm)
                .SyncObjectState(ObjectState.Added);

            this._brandRepository.Update(brand);
        }

        public async Task  ChangeBrandName(BrandDto brandDto)
        {
            var brand = await this._brandRepository.GetAsync(brandDto.Id);
            brand
                .ChangeBrandName(brandDto.BrandName);

            // Should we really update repository if its ef we are using. because objects are already tracking ef 
             this._brandRepository.Update(brand);
        }


        public async Task ChangeMerchantVknByBrand (MerchantDto merchant)
        {
            var brand = await this._brandRepository.GetBrandAggregate(merchant.BrandId);
            brand.ChangeOrAddVknToMerchant(merchant.Id, merchant.Vkn)
                .SyncObjectState(ObjectState.Modified);
            // Should we really update repository if its ef we are using. because objects are already tracking ef 
            // this._brandRepository.Update(brand);
        }

      
    }
}
