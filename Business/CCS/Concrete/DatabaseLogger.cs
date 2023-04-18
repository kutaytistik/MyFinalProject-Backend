using Business.CCS.Abstract;
using System;

namespace Business.CCS.Concrete
{
    public class DatabaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Veritabanına Loglandı");
        }
    }
}
