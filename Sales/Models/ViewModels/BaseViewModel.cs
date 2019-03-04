namespace Sales.Models.ViewModels
{
    public class BaseViewModel
    {
        public string ErrorMessage { get; set; }
        public bool HaveError => !string.IsNullOrWhiteSpace(ErrorMessage);
    }
}
