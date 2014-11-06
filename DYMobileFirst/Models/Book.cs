using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYMobileFirst.Models
{
    public class Book
    {
        public Book()
        {
            imgs = Newtonsoft.Json.JsonConvert.SerializeObject(new List<string>());
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "书名不能空")]
        [Display(Name = "名书")]
        [StringLength(128, MinimumLength = 1, ErrorMessage = "字长要在1至128之间")]
        [Remote("CheckTitleExists", "Book", HttpMethod = "POST", ErrorMessage = "书名已存在")] // 远程验证（Ajax）  
        public string Title { get; set; }
        [Required(ErrorMessage = "日期不能空")]
        [Display(Name = "发布日期")]
        [DataType(DataType.DateTime, ErrorMessage = "不是有效的日期类型")]
        //[Range(typeof(DateTime), "1966-01-01", "2020-01-01")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [AllowHtml]
        [UIHint("tinymce")]
        [Display(Name = "出版社")]
        [Required(ErrorMessage = "出版社不能空")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "必需大写字母开头及只限英文")]
        [MaxLength]
        [Column(TypeName = "ntext")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "售价不能空")]
        [Display(Name = "售价")]
        [Range(1, 999999999)]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Display(Name = "评价")]
        [UIHint("Enum")]
        public em_staute staute { get; set; }

        [Display(Name = "签售")]
        [UIHint("Enum")]
        public em_bool pub { get; set; }

        [MaxLength]
        [Column(TypeName = "ntext")]
        public string imgs { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }
        // Navigation property
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "作者")]
        [Required]
        public string Name { get; set; }
    }

    public enum em_bool
    {
        是, 否
    }

    public enum em_staute
    {
        普通, 好, 非凡
    }
}