using AutoMapper;
using JwtTutorial.Entities;
using JwtTutorial.ViewModel;

namespace JwtTutorial.Mapping
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
        }
    }
}
