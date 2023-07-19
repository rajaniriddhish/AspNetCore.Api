﻿namespace WebApi_Versioning.Models
{
    public class CountryDtoV1
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }

    public class CountryDtoV2
    {
        public int Id { get; set; }
        public required string CountryName { get; set; }
    }
}
