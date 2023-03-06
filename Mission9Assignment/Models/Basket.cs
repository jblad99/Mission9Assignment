using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9Assignment.Models
{
    public class Basket
    {
        // Class for basket line item to be loaded into the cart
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();
        public void AddItem(Book book, int qty)
        {
            BasketLineItem line = Items
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            if (line == null)
            {
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }

        public double CalculateTotal()
        {
            //Calculate total of quantity * price
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);

            return sum;
        }
    }

    public class BasketLineItem
    {
        //declare variables for each basket line item
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
