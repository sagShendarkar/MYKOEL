using API.Helpers;

namespace iot_Domain.Helpers
{
     public class ParameterParams : PaginationParams
    {
        public string? searchPagination { get; set; }
        public string? SortBy { get; set; }
        public string? SortByName { get; set; }
        public string? searchPaginationFota { get; set; }
        public int UserId { get; set; }
        public string? Flag { get; set; }
    }
}