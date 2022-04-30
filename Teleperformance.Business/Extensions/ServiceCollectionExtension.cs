using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Teleperformance.Business.Abstract;
using Teleperformance.Business.Concrete;
using Teleperformance.Business.Helper;
using Teleperformance.Business.Mapping;
using Teleperformance.Business.Validation.Category;
using Teleperformance.Business.Validation.Product;
using Teleperformance.Business.Validation.ShoppingCart;
using Teleperformance.Business.Validation.ShoppingCartItems;
using Teleperformance.Core.Utilities.Mail;
using Teleperformance.DataAccess.Contexts;
using Teleperformance.DataAccess.Repositories.Abstract;
using Teleperformance.DataAccess.Repositories.Concrete;
using Teleperformance.Entities.Concrete;
using Teleperformance.Entities.DTOs.Category;
using Teleperformance.Entities.DTOs.Product;
using Teleperformance.Entities.DTOs.ShoppingCart;
using Teleperformance.Entities.DTOs.ShoppingCartItems;

namespace Teleperformance.Business.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TeleperformanceDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddIdentity<User, Role>(opt =>
            {
                //User-Password Options
                opt.Password.RequireDigit = true;   //rakam bulunmalı mı?
                opt.Password.RequiredLength = 8;
                opt.Password.RequiredUniqueChars = 0; //özel karakter bulundurmalı mı?
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;

                //Username-Email Options
                opt.User.AllowedUserNameCharacters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789!@#$%^&*()-_=+<,>.";
                opt.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<TeleperformanceDbContext>().AddErrorDescriber<CustomIdentityErrorDescriber>();


            services.AddTransient<IMailSender, MailSender>();

            //SERVICES

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductRepository, EfProductRepository>();

            services.AddScoped<IShoppingCartService, ShoppingCartManager>();
            services.AddScoped<IShoppingCartRepository, EfShoppingCartRepository>();

            services.AddScoped<IShoppingCartItemService, ShoppingCartItemManager>();
            services.AddScoped<IShoppingCartItemRepository, EfShoppingCartItemRepository>();

            //VALIDATION
            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateValidator>();

            services.AddTransient<IValidator<ProductAddDto>, ProductAddValidator>();
            services.AddTransient<IValidator<ProductUpdateDto>, ProductUpdateValidator>();


            services.AddTransient<IValidator<ShoppingCartItemAddDto>, ShoppingCartItemAddValidator>();
            services.AddTransient<IValidator<ShoppingCartItemUpdateDto>, ShoppingCartItemUpdateValidator>();

            services.AddTransient<IValidator<ShoppingCartAddDto>, ShoppingCartAddValidator>();
            services.AddTransient<IValidator<ShoppingCartUpdateDto>, ShoppingCartUpdateValidator>();

            //MAPPINGS
            services.AddAutoMapper(
                typeof(CategoryProfile),
                typeof(ProductProfile),
                typeof(ShoppingCartProfile),
                typeof(ShoppingCartItemProfile)
           );

            return services;
        }
    }
}
