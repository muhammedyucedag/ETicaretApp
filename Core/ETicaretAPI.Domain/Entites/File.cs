using ETicaretAPI.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Entites
{
    public class File : BaseEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        [NotMapped] // File ile migration oluşursa UpdateDate ekleme 
        public override DateTime UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
