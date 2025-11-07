namespace Classes {
    public class Stock
    {
        private decimal _price;
        private string _symbol = "";
        private int _shares;
        public string Name { get; set; } = "";

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Stock price cannot be negative!");
                }
                _price = value;
                Console.WriteLine($" Stock price updated to ${value:F2}");
            }
        }
        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value?.ToUpper() ?? ""; }
        }

        public string CompanyName { get; set; } = "";
        
        public decimal CurrentPrice
        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Stock price cannot be negative!");
                }
                _price = value;
                Console.WriteLine($"Stock price updated to {value:F2}");
            }
        }

        public int SharesOwned
        {
            get { return _shares; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("");
                }
                int oldShares = _shares;
                _shares = value;

                if (oldShares != value)
                {
                    Console.WriteLine($" Shares changed from {oldShares} to {value}");
                    Console.WriteLine($" Portfolio value: ${TotalValue:F2}");

                }
            }
        }
        public DateTime CreateDate { get; init; } = DateTime.Now;
        public decimal TotalValue => CurrentPrice * SharesOwned;
    } 
} 