using System;
using System.Collections.Generic;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace OnionApp.Infrastructure.Data
{
    public class BookRepository:IBookRepository
    {
        private OrderContext db;

        public BookRepository()
        {
            this.db = new OrderContext();
        }
        public IEnumerable<Book> GetBookList()
        {
            return db.Books.ToList();
        }
        public Book GetBook(int id)
        {
            return db.Books.Find(id);
        }
        public void Create(Book book)
        {
            db.Books.Add(book);
        }
        public void Update (Book book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if(book != null)
            {
                db.Books.Remove(book);
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class NewBookRepository: IRepository<Book>
    {
        private OrderContext db;
        public NewBookRepository(OrderContext context)
        {
            this.db = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }
        public Book Get(int id)
        {
            return db.Books.Find(id);
        }
        public void Create (Book book)
        {
            db.Books.Add(book);
        }
        public void Update(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
        }
        public void Save()
        {

        }
        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if(book != null)
            {
                db.Books.Remove(book);
            }
        }
    }

    public class OrderRepository: IRepository<Order>
    {
        private OrderContext db;

        public OrderRepository(OrderContext context)
        {
            this.db = context;
        }
        public IEnumerable<Order> GetAll()
        {
            return db.Orders.Include(o => o.book);
        }
        public Order Get(int id)
        {
            return db.Orders.Find(id);
        }
        public void Create(Order order)
        {
            db.Orders.Add(order);
        }
        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
        }
        public void Save() { }

        public void Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if(order != null)
            {
                db.Orders.Remove(order);
            }
        }
    }
}
