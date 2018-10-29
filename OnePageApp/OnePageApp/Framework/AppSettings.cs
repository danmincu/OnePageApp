namespace OnePageApp.Framework
{
    public sealed class AppSettings
    {
    
        public void Load() {

            //TODO load from disk/registry the settings
            if (this.ConnectionString == null)
                this.ConnectionString = "this is the connection string";
        }

        public void Save()
        {
            //TODO save from disk/registry the settings
        }

        public string ConnectionString { get; set; }
    }
}
