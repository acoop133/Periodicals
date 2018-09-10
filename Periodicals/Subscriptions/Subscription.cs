﻿using System;
using Periodicals.Products;
using Periodicals.Users;

namespace Periodicals.Subscriptions
{
    public class Subscription
    {
        private readonly int Id;
        public User User { get; private set; }
        public Product Product { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime LastPaid { get; private set; }

        //TODO: Record none existing magazines
        public Subscription(ProductService productService, Type type, User user, string title, DateTime startDate, DateTime lastPaid)
        {
            User = user;
            if (!productService.ProductExists(title, out var product))
            {
                if (type == typeof(Magazine))
                    Product = new Magazine(title, 0);
                else if (type == typeof(Newspaper))
                    Product = new Newspaper(title, 0);
                productService.AddProduct(Product);
            }

            Product = product;
            StartDate = startDate;
            LastPaid = lastPaid;
            EndDate = new DateTime(lastPaid.Year + 1, startDate.Month, 1).AddDays(-1);
        }
    }
}
