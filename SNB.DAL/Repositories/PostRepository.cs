﻿using Microsoft.EntityFrameworkCore;
using SNB.DAL.Models;
using SNB.DAL.Repositories.IRepositories;

namespace SNB.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.Include(p => p.Tags).ToList();
        }

        public Post GetPost(Guid id)
        {
            return _context.Posts.Include(p => p.Tags).FirstOrDefault(p => p.Id == id);
        }

        public async Task AddPost(Post post)
        {
            _context.Posts?.Add(post);
            await SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _context.Posts?.Update(post);
            await SaveChangesAsync();
        }

        public async Task RemovePost(Guid id)
        {
            var post = GetPost(id);
            if (post != null)
            {
                _context.Posts?.Remove(post);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
