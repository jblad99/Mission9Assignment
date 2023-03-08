using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission9Assignment.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission9Assignment.Models
{
    public class Basket
    {
        // Class for basket line item to be loaded into the cart
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        public virtual void AddItem(Book book, int qty)
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
        public virtual void RemoveItem(Book book)
        {
            Items.RemoveAll(x => x.Book.BookId == book.BookId);

        }

        public virtual void ClearBasket()
        {
            Items.Clear();
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
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
