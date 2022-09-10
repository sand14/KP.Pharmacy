using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KP.Common.Model.Models;
using KP.Core.DomainModels;

namespace KP.Common.Model.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductModel>();

            CreateMap<ProductModel, Product>()
                .ForMember(x => x.Stock, x => x.Ignore());

            CreateMap<StockModel, Stock>();
            CreateMap<Stock, StockModel>()
                .ForMember(x=>x.Product, x=>x.Ignore());

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();


        }

    }
}
