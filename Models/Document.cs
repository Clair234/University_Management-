namespace UniversitySystem.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public int StudentID { get; set; }
        public string FileName { get; set; }
        public string FileType {  get; set; }
        public DateTime UploadDate {  get; set; }
        public DateTime UpdateDate { get; set; }= DateTime.Now;
        public string FilePath { get; set; }
    }
}
