using System;
using System.Collections.Generic;
using System.Linq;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using System.Data.Entity;

namespace OnionApp.Infrastructure.Data
{
    public class UserRepository: IRepository<User>
    {
        OrderContext db;
        public UserRepository()
        {
            db = new OrderContext();
        }
        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
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
}
