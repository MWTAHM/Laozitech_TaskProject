using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Core.TableModels
{
    public class FileModel
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public DateTime FileAddingTime { get; set; }
        public byte[] FileData { get; private set; }
        public string UserId { get; set; }
        public string ProjectId { get; set; }
        public string TaskId { get; set; }


        // Not in db [NotMapped]
        public string FullFileName
        {
            get => $"{FileName}{FileExtension}";
            set
            {
                FileName = Path.GetFileNameWithoutExtension(value);
                FileExtension = Path.GetExtension(value);
            }
        }
        public bool IsForProject => ProjectId != null;
        public bool IsForTask => TaskId != null;
        public byte[] DataFromBytes
        {
            set
            {
                FileData = value;
            }
        }
        public FileStream DataFromFile
        {
            set
            {
                FileData = new byte[value.Length];
                value.Read(FileData, 0, FileData.Length);
                value.Close();
            }
        }
        public bool DataFromStream(Stream stream, int contentLength)
        {
            try
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    FileData = binaryReader.ReadBytes(contentLength);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public FileStream FileFromData
        {
            get
            {
                MemoryStream memStream = new MemoryStream();
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(FileData, 0, FileData.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return binForm.Deserialize(memStream) as FileStream;
            }
        }
    }
}
