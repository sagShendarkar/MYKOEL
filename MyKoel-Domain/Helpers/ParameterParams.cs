using API.Helpers;

namespace iot_Domain.Helpers
{
     public class ParameterParams : PaginationParams
    {
        public string? searchPagination { get; set; }
        public string? SortBy { get; set; }
        public string? SortByName { get; set; }
        public int UserId { get; set; }
        public string? Flag { get; set; }
        public string? Location { get; set; }
        public string? Grade { get; set; }
        public int? Day {get;set;}

    }
}