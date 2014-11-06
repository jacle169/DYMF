using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace DYMobileFirst.Models
{
    public class BookDBContext : DbContext
    {
        public BookDBContext()
            : base("name=BookDBContext")
        {
            if (!File.Exists(HostingEnvironment.MapPath(@"~\App_Data\data.sdf")))
            {
                Database.SetInitializer<BookDBContext>(new SystemInitializer());
            }
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
    }

    public class SystemInitializer : DropCreateDatabaseIfModelChanges<BookDBContext>
    {
        protected override void Seed(BookDBContext context)
        {
            //Id标识从1000开始
            context.Database.ExecuteSqlCommand("ALTER TABLE [Books] ALTER COLUMN [Id] IDENTITY (1001,1)");

            context.SystemUsers.Add(new SystemUser() { ot_Name = "管理员", ot_userId = "admin", ot_pwd = "admin", ot_skin = em_opSkin.管理员 });

            var d = new Author() { Name = "古龙" };
            var c = new Author() { Name = "金庸" };
            var e = new Author() { Name = "梁羽生" };
            context.Authors.Add(d);
            context.Authors.Add(c);
            context.Authors.Add(e);

            context.Books.Add(
                new Book()
                {
                    Title = "射雕英雄传（全四册）（新修版）",
                    Price = 61.9m,
                    AuthorId = c.Id,
                    Genre = "广州出版社",
                    Author = c,
                    ReleaseDate = new DateTime(2013, 2, 1),
                    staute = em_staute.好,
                    pub =  em_bool.是
                });
            context.Books.Add(new Book()
            {
                Title = "梁羽生作品集（全集73册，共两箱）",
                Price = 1494.4m,
                AuthorId = c.Id,
                Genre = "中山大学出版社",
                Author = c,
                ReleaseDate = new DateTime(2012, 5, 1),
                staute = em_staute.普通,
                pub = em_bool.否
            });
            context.Books.Add(new Book()
            {
                Title = "绝代双骄",
                Price = 85m,
                Genre = "河南文艺出版社",
                AuthorId = d.Id,
                Author = d,
                ReleaseDate = new DateTime(2013, 9, 1),
                staute = em_staute.普通,
                pub =  em_bool.否
            });
            context.Books.Add(
                new Book()
                {
                    Title = "射雕英雄传（全四册）（新修版）",
                    Price = 61.9m,
                    AuthorId = c.Id,
                    Genre = "广州出版社",
                    Author = c,
                    ReleaseDate = new DateTime(2013, 2, 1),
                    staute = em_staute.好,
                    pub = em_bool.是
                });
            context.Books.Add(new Book()
            {
                Title = "梁羽生作品集（全集73册，共两箱）",
                Price = 1494.4m,
                AuthorId = c.Id,
                Genre = "中山大学出版社",
                Author = c,
                ReleaseDate = new DateTime(2012, 5, 1),
                staute = em_staute.普通,
                pub = em_bool.否
            });
            context.Books.Add(new Book()
            {
                Title = "绝代双骄",
                Price = 85m,
                Genre = "河南文艺出版社",
                AuthorId = d.Id,
                Author = d,
                ReleaseDate = new DateTime(2013, 9, 1),
                staute = em_staute.普通,
                pub = em_bool.否
            }); context.Books.Add(
                 new Book()
                 {
                     Title = "射雕英雄传（全四册）（新修版）",
                     Price = 61.9m,
                     AuthorId = c.Id,
                     Genre = "广州出版社",
                     Author = c,
                     ReleaseDate = new DateTime(2013, 2, 1),
                     staute = em_staute.好,
                     pub = em_bool.是
                 });
            context.Books.Add(new Book()
            {
                Title = "梁羽生作品集（全集73册，共两箱）",
                Price = 1494.4m,
                AuthorId = c.Id,
                Genre = "中山大学出版社",
                Author = c,
                ReleaseDate = new DateTime(2012, 5, 1),
                staute = em_staute.普通,
                pub = em_bool.否
            });
            context.Books.Add(new Book()
            {
                Title = "绝代双骄",
                Price = 85m,
                Genre = "河南文艺出版社",
                AuthorId = d.Id,
                Author = d,
                ReleaseDate = new DateTime(2013, 9, 1),
                staute = em_staute.普通,
                pub = em_bool.否
            }); context.Books.Add(
                 new Book()
                 {
                     Title = "射雕英雄传（全四册）（新修版）",
                     Price = 61.9m,
                     AuthorId = c.Id,
                     Genre = "广州出版社",
                     Author = c,
                     ReleaseDate = new DateTime(2013, 2, 1),
                     staute = em_staute.好,
                     pub = em_bool.是
                 });
            context.Books.Add(new Book()
            {
                Title = "梁羽生作品集（全集73册，共两箱）",
                Price = 1494.4m,
                AuthorId = c.Id,
                Genre = "中山大学出版社",
                Author = c,
                ReleaseDate = new DateTime(2012, 5, 1),
                staute = em_staute.普通,
                pub = em_bool.否
            });
            context.Books.Add(new Book()
            {
                Title = "绝代双骄",
                Price = 85m,
                Genre = "河南文艺出版社",
                AuthorId = d.Id,
                Author = d,
                ReleaseDate = new DateTime(2013, 9, 1),
                staute = em_staute.普通,
                pub = em_bool.否
            }); context.Books.Add(
                 new Book()
                 {
                     Title = "射雕英雄传（全四册）（新修版）",
                     Price = 61.9m,
                     AuthorId = c.Id,
                     Genre = "广州出版社",
                     Author = c,
                     ReleaseDate = new DateTime(2013, 2, 1),
                     staute = em_staute.好,
                     pub = em_bool.是
                 });
            context.Books.Add(new Book()
            {
                Title = "梁羽生作品集（全集73册，共两箱）",
                Price = 1494.4m,
                AuthorId = c.Id,
                Genre = "中山大学出版社",
                Author = c,
                ReleaseDate = new DateTime(2012, 5, 1),
                staute = em_staute.普通,
                pub = em_bool.否
            });
            context.Books.Add(new Book()
            {
                Title = "绝代双骄",
                Price = 85m,
                Genre = "河南文艺出版社",
                AuthorId = d.Id,
                Author = d,
                ReleaseDate = new DateTime(2013, 9, 1),
                staute = em_staute.普通,
                pub = em_bool.否
            });
            context.SaveChanges();
        }

    }
}