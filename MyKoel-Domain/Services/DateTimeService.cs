 
using MyKoel_Domain.Interfaces;

namespace MyKoel_Domain.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }

}
