using Shop.Web.Models;
using Shop.Web.Models.Dto;

namespace Shop.Web.Services.IServices
{
    public interface IBaseService:IDisposable
    {
        ResponseDto responseModel { get;set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
