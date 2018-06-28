namespace XDetroit.WebFrontend.Dal
{
    public class ProductItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExtCategoryId { get; set; }
        public ItemCategory ExtCategory { get; set; }
    }
}