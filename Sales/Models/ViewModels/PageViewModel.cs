namespace Sales.Models.ViewModels
{
    public class PageViewModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }

        public PageViewModel(int pageNumber, int pageSize, int totalPages)
        {
            PageNumber = pageNumber;
            TotalPages = totalPages;
        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;   
        public bool StartPageIsFar => PageNumber > 2;
        public bool EndPageIsFar => PageNumber + 1 < TotalPages;
        public bool HasBooks => TotalPages > 0;
    }
}
