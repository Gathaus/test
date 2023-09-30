namespace POI.Domain.Entities;

 public class Demography : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string? Cat1 { get; set; }

        public string? Cat2 { get; set; }

        public string? Cat3 { get; set; }

        public string? Cat4 { get; set; }

        public string? Cat5 { get; set; }

        public int CityCode { get; set; }

        public int CountyCode { get; set; }

        public int DistrictCode { get; set; }

        public string CityName { get; set; }

        public string CountyName { get; set; }

        public string DistrictName { get; set; }
        
        public int? TotalPopulation { get; set; }

        public int? MalePopulation { get; set; }

        public int? FemalePopulation { get; set; }

        public int? UrbanPopulation { get; set; }

        public int? RuralPopulation { get; set; }
        
    }