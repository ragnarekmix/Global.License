namespace Global.LicenseManager.Common.Entities
{
    public class License
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Key { get; set; }
        public string CreationDate { get; set; }
        public string ModificationDate { get; set; }
    }
}
