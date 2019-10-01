using AutoMapper;
using Core.Identity.Models;
using Core.ViewModels.GestaoAcesso;

namespace Core.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ControleAcesso, ControleAcessoViewModel>();
        }
    }
}