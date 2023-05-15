using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.RequestParameters
{
    //Record, genellikle veri transfer nesneleri, veri erişim katmanı sınıfları veya değiştirilemez değerler gibi veri odaklı senaryolarda kullanılır.
    public record Pagination
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
