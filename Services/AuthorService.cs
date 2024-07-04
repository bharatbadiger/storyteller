using Storyteller.Models;
using Storyteller.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Storyteller.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(long id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<Author> AddAsync(Author author)
        {
            var existingAuthor = await _authorRepository.GetByNameAsync(author.Name);
            if (existingAuthor != null)
            {
                throw new Exception("Author with this name already exists.");
            }

            await _authorRepository.AddAsync(author);
            return author;
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            await _authorRepository.UpdateAsync(author);
            return author;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return false;

            await _authorRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<Author>> GetByNameAsync(string name)
        {
            return await _authorRepository.GetByNameContainingAsync(name);
        }
    }
}
