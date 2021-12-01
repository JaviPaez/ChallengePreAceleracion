using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication;
using WebApplication.Models;

namespace Data.Repositories
{
    public class CharacterRepository : GenericRepository<Character, ApplicationDbContext> , ICharacterRepository
    {
        private readonly ApplicationDbContext _context;

        //Constructors
        public CharacterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        //Methods
        IEnumerable<Character> ICharacterRepository.GetAll()
        {
            return _context.Characters.ToList();
        }

        public Character Get(int CharacterId)
        {
            return _context.Characters.Find(CharacterId);
        }

        public void Insert(Character character)
        {
            _context.Characters.Add(character);
        }

        public void Update(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
        }

        void ICharacterRepository.Delete(int CharacterId)
        {
            Character character = _context.Characters.Find(CharacterId);
            _context.Characters.Remove(character);
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        //
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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