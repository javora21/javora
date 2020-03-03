using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace javora.Models.View
{
    public class NewsModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [MaxLength(100,ErrorMessage = "Забагато символів")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        public IFormFile MainImage { get; set; }

        public string MainImagePath { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        [MaxLength(250,ErrorMessage = "Забагато символів")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обов'язкове поле")]
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
