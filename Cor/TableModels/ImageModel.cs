using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Core.TableModels
{
    public class ImageModel
    {
        public string ImageId { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileExtension { get; set; }
        public DateTime ImageAddingTime { get; set; }
        public byte[] ImageFileData { get; private set; }
        public string UserId { get; set; }
        public string ProjectId { get; set; }
        public string TaskId { get; set; }

        // Not in DB
        public string FullImageName
        {
            get => $"{ImageFileName}{ImageFileExtension}";
            set
            {
                if (value!= null)
                {
                    ImageFileName = Path.GetFileNameWithoutExtension(value);
                    ImageFileExtension = Path.GetExtension(value);
                }
            }
        }
        public bool IsForProject => ProjectId != null;
        public bool IsForTask => TaskId != null;
        public bool DataFromStream(Stream stream, int contentLength)
        {
            try
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    ImageFileData = binaryReader.ReadBytes(contentLength);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public byte[] DataFromBytes
        {
            set
            {
                ImageFileData = value;
            }
        }
        public FileStream DataFromImage
        {
            set
            {
                ImageFileData = new byte[value.Length];
                value.Read(ImageFileData, 0, ImageFileData.Length);
                value.Close();
            }
        }
        public FileStream ImageFromData
        {
            get
            {
                MemoryStream memStream = new MemoryStream();
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(ImageFileData, 0, ImageFileData.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return binForm.Deserialize(memStream) as FileStream;
            }
        }
    }
}
