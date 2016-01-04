﻿using MWS.Domain.Entidades;
using MWS.Domain.Repositories;
using MWS.Infra.Persistence.DataContexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MWS.Infra.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public List<Category> Get()
        {
            return _context.Categories.ToList();
        }

        public List<Category> Get(int skip, int take)
        {
            return _context.Categories.OrderBy(x => x.Title).Skip(skip).Take(take).ToList();
        }

        public Category Get(int id)
        {
            return _context.Categories.Find(id);
        }

        public void Create(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}