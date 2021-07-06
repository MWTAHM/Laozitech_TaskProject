using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.TableModels
{
    class ImageModel
    {
        public string ImageId { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileExtension { get; set; }
        public DateTime AddingTime { get; set; }
        public byte[] ImageFileData { get; set; }

        // Not in DB
        public string FullImageName
            => $"{ImageFileName}.{ImageFileExtension}";

    }
}
