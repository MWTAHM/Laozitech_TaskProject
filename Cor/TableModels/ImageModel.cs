using System;
using System.IO;

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
                if (value != null)
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
    }
}
