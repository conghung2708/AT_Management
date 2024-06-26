﻿using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IImageRepository 
    {
        Task<Image> Upload(Image image);
    }
}
