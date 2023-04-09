using AutoMapper;
using SalonNamjestaja.Data;
using SalonNamjestaja.Models;
using SalonNamjestaja.Models.AddressModel;
using SalonNamjestaja.Models.BillModel;
using SalonNamjestaja.Models.BonusCardModel;
using SalonNamjestaja.Models.CategoryModel;
using SalonNamjestaja.Models.CustomerModel;
using SalonNamjestaja.Models.OrderItemModel;
using SalonNamjestaja.Models.OrderModel;
using SalonNamjestaja.Models.ProductModel;
using SalonNamjestaja.Models.UserTypeModel;

namespace SalonNamjestaja.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            //PROIZVOD
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<AddProductRequest, Product>().ReverseMap();
            CreateMap<UpdateProductRequest, Product>().ReverseMap();

            //ADRESA
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<AddAddressRequest, Address>().ReverseMap();
            CreateMap<UpdateAddressRequest, Address>().ReverseMap();

            //BONUS KARTICA
            CreateMap<BonusCard, BonusCardDto>().ReverseMap();
            CreateMap<AddCardRequest, BonusCard>().ReverseMap();
            CreateMap<UpdateCardRequest, BonusCard>().ReverseMap();

            //PORUDZBINA
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<AddOrderRequest, Order>().ReverseMap();
            CreateMap<UpdateOrderRequest, Order>().ReverseMap();

            //KATEGORIJA PROIZVODA
            CreateMap<ProductCategory, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequest, ProductCategory>().ReverseMap();
            CreateMap<UpdateCategoryRequest, ProductCategory>().ReverseMap();

            //KORISNIK
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<AddCustomerRequest, Customer>().ReverseMap();
            CreateMap<UpdateCustomerRequest, Customer>().ReverseMap();

            //STAVKA PORUDZBINE
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<AddOrderItemRequset, OrderItem>().ReverseMap();
            CreateMap<UpdateOrderItemRequest, OrderItem>().ReverseMap();

            //PORUDZBINA (RACUN)
            CreateMap<Bill, BillDto>().ReverseMap();
            CreateMap<AddBillRequest, Bill>().ReverseMap();
            CreateMap<UpdateBillRequest, Bill>().ReverseMap();

            //TIP KORISNIKA
            CreateMap<UserType, UserTypeDto>().ReverseMap();
            CreateMap<AddUserTypeRequest, UserType>().ReverseMap();
            CreateMap<UpdateUserTypeRequest, UserType>().ReverseMap();

            CreateMap<Customer, RegisterRequestDto>().ReverseMap();
            CreateMap<Customer, LoginRequestDto>().ReverseMap();
            CreateMap<Customer, AdminRequestDto>().ReverseMap();
        }
    }
}
