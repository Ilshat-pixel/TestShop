namespace Shop.Services.CartApi.Models.Dto
{
    public class ResponseDto
    {
        public bool IsSucces { get; set; } = true;
        public object Result { get; set; } = "";
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }
    }
}
