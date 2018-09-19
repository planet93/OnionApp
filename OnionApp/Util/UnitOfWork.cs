using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnionApp.Infrastructure.Data;

namespace OnionApp.Util
{
    public class UnitOfWork:IDisposable
    {
        private OrderContext db = new OrderContext();
        private NewBookRepository bookRepository;
        private OrderRepository orderRepository;

        public NewBookRepository Books
        {
            get
            {
                if(bookRepository == null) { bookRepository = new NewBookRepository(db); }
                return bookRepository;
            }
        }
        public OrderRepository Orders
        {
            get
            {
                if (orderRepository == null) { orderRepository = new OrderRepository(db); }
                return orderRepository;
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
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}