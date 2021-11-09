namespace ShopSolution.Core.Specifications
{
    public class ProductSpecParams
    {
        private const int _maxPageSize = 50;
        private int _pageSize = 6;
        private string _search;
        public int PageIndex { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
        }

        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }
        
    }
}