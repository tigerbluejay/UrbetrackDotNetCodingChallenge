﻿namespace NetExam.Dto
{
    using System;

    public class LocationSpecs
    {
        public string Name { get; }
        public string Neighborhood { get; }

        public LocationSpecs(string name, string neighborhood)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));
            if (string.IsNullOrWhiteSpace(neighborhood)) throw new ArgumentException(nameof(neighborhood));

            Name = name;
            Neighborhood = neighborhood;
        }
    }
}