using AutoMapper;
using MyApp.Application.Common.Service;
using MyApp.Application.Features.Administrative.Responses;
using MyApp.Domain.Core.Repositories;
using MyApp.Domain.Entities.Districts;
using MyApp.Domain.Specifications.Administratives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MyApp.Application.Features.Administrative
{
    public class AdministrativeServcie : IAdministrativeServcie
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdministrativeServcie(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

         
        }

        public async Task SeedAsync(CancellationToken ct = default)
        {
           
            var spec = new AdministrativeSpec();
            var levelRepo = _unitOfWork.Repository<AdministrativeLevel, int>();
            var provinceRepo = _unitOfWork.Repository<Province, int>();
            var communeRepo = _unitOfWork.Repository<Commune, int>();

            // tránh seed lại (check 1 lần duy nhất)
            if (await levelRepo.AnyAsync(spec, ct))
                return;

            // =========================
            // 1. Seed AdministrativeLevel
            // =========================
            var levels = new List<AdministrativeLevel>
            {
                new() { Code = "CITY", Name = "Thành phố Trung ương" },
                new() { Code = "PROVINCE", Name = "Tỉnh" },
                new() { Code = "WARD", Name = "Phường" },
                new() { Code = "COMMUNE", Name = "Xã" },
                new (){ Code = "SAR" , Name ="Đặc khu"}
             };

            await levelRepo.AddRangeAsync(levels, ct);
            await _unitOfWork.SaveChangesAsync(ct); // cần để có Id

            // =========================
            // 2. Build mapping (Name → Id)
            // =========================
            var levelDict = levels.ToDictionary(
                x => Normalize(x.Name),
                x => x.Id
            );

            // =========================
            // 3. Đọc JSON
            // =========================

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            // =========================
            var provinceJson = await File.ReadAllTextAsync("Data/provinces.json", ct);
            var communeJson = await File.ReadAllTextAsync("Data/communes.json", ct);

            var provinceData = JsonSerializer.Deserialize<ProvinceJsonResponse>(provinceJson, options)
                                ?? throw new Exception("Invalid provinces.json");

            var communeData = JsonSerializer.Deserialize<CommuneJsonResponse>(communeJson, options)
                                ?? throw new Exception("Invalid communes.json");
            // =========================
            // 4. Map Province
            // =========================
            var provinces = provinceData.Provinces.Select(p => new Province
            {
                Code = p.Code,
                Name = p.Name,
                EnglishName = p.EnglishName,
                AdministrativeLevelId = GetLevelId(p.AdministrativeLevel, levelDict)
            }).ToList();

            await provinceRepo.AddRangeAsync(provinces, ct);

            // =========================
            // 5. Map Commune
            // =========================
            var communes = communeData.Communes.Select(c => new Commune
            {
                Code = c.Code,
                Name = c.Name,
                EnglishName = c.EnglishName,
                AdministrativeLevelId = GetLevelId(c.AdministrativeLevel, levelDict),
                ProvinceCode = c.ProvinceCode
            }).ToList();

            await communeRepo.AddRangeAsync(communes, ct);

            // =========================
            // 6. Save 1 lần duy nhất
            // =========================
            await _unitOfWork.SaveChangesAsync(ct);
        }

        // =========================
        // Helper
        // =========================
        private static int GetLevelId(string levelName, Dictionary<string, int> levelDict)
        {
            var key = Normalize(levelName);

            if (!levelDict.TryGetValue(key, out var id))
                throw new Exception($"AdministrativeLevel not found: {levelName}");

            return id;
        }

        private static string Normalize(string value)
        {
            return value.Trim().ToLowerInvariant();
        }
    }

}