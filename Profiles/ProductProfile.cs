using AutoMapper;
using ReviewProdutosEcommerce.API.Entities;
using ReviewProdutosEcommerce.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewProdutosEcommerce.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductReview, ProductReviewViewModel>();
            CreateMap<ProductReview, ProductReviewDetailsViewModel>();

            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductDetailsViewModel>();
        }
    }
}
