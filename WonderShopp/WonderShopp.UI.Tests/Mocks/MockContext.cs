﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using WonderShopp.Core.Contracts;
using WonderShopp.Core.Models;

namespace WonderShopp.UI.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;
        public MockContext()
        {
            items = new List<T>();
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Commit()
        {
            return;
        }

        public void Delete(string Id)
        {
            T tToDelete = items.Find(i => i.Id == Id);
            if (tToDelete == null)
                items.Remove(tToDelete);
            else
                throw new Exception(className + " Not Found");
        }

        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)
                return t;
            else
                throw new Exception(className + " Not Found");
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpate = items.Find(i => i.Id == t.Id);
            if (tToUpate != null)
                tToUpate = t;
            else
                throw new Exception(className + " Not Found");
        }
    }
}
