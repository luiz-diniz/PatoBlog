﻿using BlogApp.Models;

namespace BlogApp.Core.Intefaces;

public interface IPostService
{
    void Add(Post post);
    IEnumerable<Post> GetAll();
}