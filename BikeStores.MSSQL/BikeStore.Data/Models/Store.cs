﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace BikeStore.Data.Models
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
            Staff = new HashSet<Staff>();
            Stocks = new HashSet<Stock>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}