namespace LMS_Elibrary.Models
{
    public class FileForDownload
    {
        public byte[] bytes {  get; set; }
        public string contentType { get; set; }
        public string fileName { get; set; }

        public FileForDownload(byte[] _bytes, string _contentType, string _fileName)
        {
            bytes = _bytes;
            contentType = _contentType;
            fileName = _fileName;
        }
    }
}
