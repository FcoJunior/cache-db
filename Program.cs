using System;
using cache_db.Commands;

namespace cache_db
{
    class Program
    {
        static void Main(string[] args)
        {
            new Startup().Execute();
        }
    }
}
