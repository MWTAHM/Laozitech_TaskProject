using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TableModels
{
    class FileModel
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public DateTime AddingTime { get; set; }
        public byte[] FileDate { get; set; }


        // Not in db [NotMapped]
        public string FullFileName
            => $"{FileName}.{FileExtension}";
    }
}
