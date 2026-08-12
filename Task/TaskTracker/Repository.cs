using System;
using System.Collections.Generic;

namespace TaskTracker
{
    public class Repository<T>
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> GetAll()
        {
            return items;
        }

        public List<T> FindAll(Predicate<T> predicate)
        {
            return items.FindAll(predicate);
        }
    }
}