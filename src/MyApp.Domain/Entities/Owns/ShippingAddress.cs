using MyApp.Domain.Abstractions;
using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities.Owns
{
    public class ShippingAddress
    {

        private ShippingAddress() { }
        private ShippingAddress(
            string provinceCode,
            string provinceName,
            string communeCode,
            string communeName,
            string detail)
        {
            ProvinceCode = provinceCode;
            ProvinceName = provinceName;
            CommuneCode = communeCode;
            CommuneName = communeName;
            Detail = detail;
        }

        public string ProvinceCode { get; private set; } = null!;
        public string ProvinceName { get; private set; } = null!;
        public string CommuneCode { get; private set; } = null!;
        public string CommuneName { get; private set; } = null!;
        public string Detail { get; private set; } = null!;

        public static ShippingAddress Create(
            string provinceCode,
            string provinceName,
            string communeCode,
            string communeName,
            string detail)
        {
            if (string.IsNullOrWhiteSpace(provinceCode))
                throw new ArgumentException("Province code is required");

            if (string.IsNullOrWhiteSpace(provinceName))
                throw new ArgumentException("Province name is required");

            if (string.IsNullOrWhiteSpace(communeCode))
                throw new ArgumentException("Commune code is required");

            if (string.IsNullOrWhiteSpace(communeName))
                throw new ArgumentException("Commune name is required");

            if (string.IsNullOrWhiteSpace(detail))
                throw new ArgumentException("Address detail is required");

            // normalize
            provinceCode = provinceCode.Trim();
            provinceName = provinceName.Trim();
            communeCode = communeCode.Trim();
            communeName = communeName.Trim();
            detail = detail.Trim();

            return new ShippingAddress(
                provinceCode,
                provinceName,
                communeCode,
                communeName,
                detail
            );
        }
    }

}
