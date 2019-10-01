using AutoMapper;
using Core.Identity.Models;
using Core.ViewModels.GestaoAcesso;

namespace Core.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ControleAcessoViewModel, ControleAcesso>();
        }
    }
}