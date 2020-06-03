using System.IO;

namespace StarTrack.Dashboard.Models
{
    public class AssetImageModelCreate
    {
        public int Id { get; set; }
        public Stream Content { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string FullPath
        {
            get
            {
                return System.IO.Path.Combine(System.Configuration.ConfigurationManager.AppSettings["IMAGE_PATH"], this.Path);
            }
        }
        public string Path
        {
            get
            {
                return System.IO.Path.Combine(this.Id.ToString(), this.Name);
            }
        }

    }

    public class AssetImageModelView
    {
        public string Path { get; set; }
        public string FullPath
        {
            get
            {
                return System.IO.Path.Combine(System.Configuration.ConfigurationManager.AppSettings["IMAGE_PATH"], this.Path);
            }
        }
    }
}